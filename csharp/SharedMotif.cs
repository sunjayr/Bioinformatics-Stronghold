/*
Problem: Given a collection of k DNA strings. Return the longest common stubstring.
Solution: Create a suffix tree with all suffixes of the strings. Identify the 
Lowest Common Ancestor of the nodes and return the reversed path to the roots
to identify the longest common substring.
 */
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rosalind
{   
    class SuffixTreeNode{
        public string element;
        public Hashtable children;
        public SuffixTreeNode parent;
        public HashSet<int> stringsShared;

        public SuffixTreeNode(string element, SuffixTreeNode parent){
            this.element = element;
            children = new Hashtable();
            this.parent = parent;
            stringsShared = new HashSet<int>();
        }

        public void PutChild(string element, SuffixTreeNode node){
            this.children[element] = node;
        }

        public bool HasChild(string element){
            if(children.ContainsKey(element)){
                return true;
            }
            else{
                return false;
            }
        }
    }
    
    public class SharedMotif
    {
        private string fastaFile;
        private ArrayList sequences; 
        private ArrayList subsequences;
        private SuffixTreeNode root;

        public SharedMotif(string fastaFile){
            this.fastaFile = fastaFile;
            this.sequences = new ArrayList();
            this.subsequences = new ArrayList();
        }

        public void ParseFastaFile(){
            StreamReader fastaReader = new StreamReader(this.fastaFile);
            string line;
            string sequenceBuffer = "";
            while((line = fastaReader.ReadLine()) != null){
                if(line[0] == '>'){
                    if( sequenceBuffer != ""){
                        sequences.Add(sequenceBuffer);
                        sequenceBuffer = "";
                    }
                }
                else{
                    sequenceBuffer += line;
                }
            }
            if(sequenceBuffer != ""){
                sequences.Add(sequenceBuffer);
            }

        }

        public void AppendTerminator(){
            int stringCount = 0;
            for(int i = 0; i < sequences.Count; i++){
                sequences[i] += $"{stringCount}";
                stringCount++;
            }
        }

        public void CreateSubsequences(){
            foreach (string seq in sequences){ 
                for(int i = 0; i < seq.Length; i++){
                    string subSeq = seq.Substring(i,seq.Length - i);
                    if(!subSeq.All(char.IsDigit)){
                        subsequences.Add(subSeq);
                    }
                }
            }
        }

        public void BuildSuffixTree(){
            this.root = new SuffixTreeNode("$", null);
            SuffixTreeNode currentNode;
            foreach (string seq in subsequences){
                currentNode = this.root;
                int stringNumber = 99999;
                for (int i = 0; i < seq.Length; i++){
                    string letter = seq[i].ToString();
                    if(int.TryParse(letter, out stringNumber)){
                        stringNumber = int.Parse(seq.Substring(i, seq.Length - i));
                        break;
                    }
                    
                    if(currentNode.HasChild(letter)){
                        currentNode = (SuffixTreeNode) currentNode.children[letter];
                    }
                    else {
                        SuffixTreeNode insert = new SuffixTreeNode(letter, currentNode);
                        currentNode.PutChild(letter, insert);
                        currentNode = insert;
                    }
                    
                }

                while(currentNode.parent != null){
                    currentNode.stringsShared.Add(stringNumber);
                    currentNode = currentNode.parent;
                }
                
            }
        }

        public void LevelOrderTraversal(){
            Queue<SuffixTreeNode> queue = new Queue<SuffixTreeNode>(); 
            queue.Enqueue(this.root);
            SuffixTreeNode currentNode;
            while (queue.Count > 0){
                currentNode = queue.Dequeue();
                foreach (SuffixTreeNode node in currentNode.children.Values){
                    queue.Enqueue(node);
                }
            }
        }
        
        public void LocatePrefixNode(){
            Queue<SuffixTreeNode> toVisit = new Queue<SuffixTreeNode>();
            Stack<SuffixTreeNode> visited = new Stack<SuffixTreeNode>();
            string result = "";
            
            toVisit.Enqueue(this.root);
            SuffixTreeNode currentNode = null;
            while(toVisit.Count > 0){
                currentNode = toVisit.Dequeue();
                foreach (SuffixTreeNode node in currentNode.children.Values){
                    toVisit.Enqueue(node);
                }
                visited.Push(currentNode);
            }
    
            while (visited.Count > 0){
                currentNode = visited.Pop();
                if(currentNode.stringsShared.Count == sequences.Count){
                    break;
                }
            }

            while (currentNode.parent != null){
                result += currentNode.element;
                currentNode = currentNode.parent;
            }
            
            char[] answer = result.ToCharArray();
            Array.Reverse(answer);
            Console.WriteLine(new string(answer));
        }

        public void Execute(){
            this.ParseFastaFile();
            this.AppendTerminator();
            this.CreateSubsequences();
            this.BuildSuffixTree();
            this.LocatePrefixNode();

        }
    }
}