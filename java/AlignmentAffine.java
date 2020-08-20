import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

/*
Program Name: AlignmentAffine.java
Purpose: This program performs an alignment between two amino acid strings with
affine gap penalities involved. Given two hardcoded amino acid strings, 
the dynamic programming based algorithm defines three two dimensional matrices
each with the row length being the length of string1 + 1 and the column length being
the length of string2 + 1. Using the BLOSUM62 scoring matrix, the alignment giving the
maximum possible score is calculated and the alignment is reconstructed using 
memoization. The BLOSUM62 scoring matrix provides a score for each substitution
of an amino acid. 

*/
public class AlignmentAffine {
    
    public static void main( String[] args) throws FileNotFoundException, IOException{
        String fileName = "BLOSUM62.txt";
        FileReader reader = new FileReader(fileName);
        BufferedReader bReader = new BufferedReader(reader);
        
        String line;
        ArrayList<String[]> inputLines = new ArrayList<String[]>();
        ArrayList<String> finalStrings;
        
        // define the amino acid strings
        String firstString = "WEEYSIDYPWDAGLTHMNGQFQLFEVKSAVSKDTMTYKEVYEQRHSYLVYELLSINQGRILAARFENIFSICH";
        String secondString = "WEEYSIDYPWDFGLTHMYGQFQLFEVKMTYSYLVYELLSINQGRILAWWTSRFENIFSICH";
        
        //read in the BLOSUM62 scoring matrix from the file
        while( (line = bReader.readLine()) != null){
            inputLines.add(line.split(" "));
        }
        
        //define matrices for scoring matrix and output
        int[][] scoringMatrix = new int[20][20];
        int[][] output;
        int counter = 0;
        
        //read in the BLOSUM62 scores into the scoring matrix
        for( String[] d:inputLines){
            for( int i = 0; i < 20; i++){
                scoringMatrix[counter][i] = Integer.parseInt(d[i]);
            }
            counter++;
        }
        //pass in values and get the array of scores back
        output = AffineGap(firstString,secondString,scoringMatrix);
        
        //get the aligned strings from memoization
        finalStrings = IterativeOutputLCS(output,firstString.length(),secondString.length(),firstString,secondString);
        
        //print out the aligned strings
        for( int a = finalStrings.get(0).length()-1; a >= 0; a--){
            System.out.print(finalStrings.get(0).charAt(a));
        }
        System.out.println();
        for( int b = finalStrings.get(1).length()-1; b >= 0; b-- ){
            System.out.print(finalStrings.get(1).charAt(b));
        }
        System.out.println();
        
    }
    //This function finds the optimal alignment through the two dimensional matrices
    public static int[][] AffineGap(String v, String w, int[][] scoringMatrix){
        //gap opening penalty
        int sigma = 11;
        
        //gap extension penalty
        int epsilon = 1;
        
        //array for insertion 
        int[][] lower = new int[v.length()+1][w.length()+1];
        
        //array for mismatch/match
        int[][] middle = new int[v.length()+1][w.length()+1];
        
        //array for deletion
        int[][] upper = new int[v.length()+1][w.length()+1];
        
        //memoization array
        int[][] Backtrack = new int[v.length()+1][w.length()+1];
        
        //Hashmap for each of the amino acids and their respective scores for use 
        //in the BLOSUM62 scoring matrix
        Map<String,Integer> matrixMap = new HashMap<String,Integer>();
        String[] aminoAcids = {"A", "C", "D", "E", "F", "G", "H", "I", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "Y"};
        
        //insert into hashmap
        for( int i = 0; i < aminoAcids.length; i++){
            matrixMap.put(aminoAcids[i], i);
        }
        
        //initialize the top row and column of the arrays
        for( int i = 0; i <= v.length(); i++){
            lower[i][0] = 0;
            middle[i][0] = 0;
            upper[i][0] = 0;
        }
        for( int j = 0; j < w.length(); j++){
            lower[0][j] = 0;
            middle[0][j] = 0;
            upper[0][j] = 0; 
        }
        
        //loop through each cell of the array
        for( int i = 1; i <= v.length(); i++){
            for( int j = 1; j <= w.length(); j++){
                //record the maximum score from each array
                lower[i][j] = Math.max(lower[i-1][j] - epsilon,middle[i-1][j] - sigma);
                upper[i][j] = Math.max(upper[i][j-1] - epsilon, middle[i][j-1] - sigma);
                middle[i][j] = Math.max(middle[i-1][j-1] + scoringMatrix[matrixMap.get(Character.toString(v.charAt(i-1)))][matrixMap.get(Character.toString(w.charAt(j-1)))],
                        Math.max(lower[i][j], upper[i][j]));
                
                
                //record the position of origin for memoization
                if( middle[i][j] == lower[i][j]){
                    Backtrack[i][j] = 1;
                }
                else if( middle[i][j] == upper[i][j]){
                    Backtrack[i][j] = 2;
                }
                else if( middle[i][j] == middle[i-1][j-1] + scoringMatrix[matrixMap.get(Character.toString(v.charAt(i-1)))][matrixMap.get(Character.toString(w.charAt(j-1)))]){
                    Backtrack[i][j] = 3;
                }
            }
        }
        
        //print out the score of the optimal alignment
        System.out.println(middle[v.length()][w.length()]);
        
        //return the memoization array
        return Backtrack;
    }
    
    
    //performs the memoization of the alignment
    public static ArrayList<String> IterativeOutputLCS(int[][] Backtrack,int n, int m, String v, String w){
        ArrayList<String> stringArray = new ArrayList<String>();
        int i = n;
        int j = m;
        String vAlignment  = "";
        String wAlignment = "";
        //loop through the memoization array from the end to the origin adding
        //the alignment to each of the strings 
        while(i > 0 && j > 0){
            if( Backtrack[i][j] == 1){
                vAlignment += Character.toString(v.charAt(i-1));
                wAlignment += "-";
                i--;
            }
            else if( Backtrack[i][j] == 2){
                vAlignment += "-";
                wAlignment += Character.toString(w.charAt(j-1));
                j--;
            }
            else if( Backtrack[i][j] == 3){ 
                vAlignment += Character.toString(v.charAt(i-1));
                wAlignment += Character.toString(w.charAt(j-1));
                i--;
                j--;
            }
            else{
                if( i == 0 && j == 0){
                    break;
                }
                
                if( i == 0){
                    vAlignment += "-";
                    wAlignment += Character.toString(w.charAt(j-1));
                    j--;
                }
                else if( j == 0){
                    vAlignment += Character.toString(w.charAt(i-1));
                    wAlignment += "-";
                    i--;
                }
            }
        }
        //add the strings to an arraylist and return the arraylist 
        //this arraylist contains the alignment
        stringArray.add(vAlignment);
        stringArray.add(wAlignment);
        
        return stringArray;
    }
    
}
