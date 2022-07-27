import java.lang.Math;
import java.io.*;
import java.util.*;
/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Sunjay
 */
public class GlobalAlignment {
    
    public static void main( String[] args ) throws FileNotFoundException, IOException{
        String fileName = "BLOSUM62.txt";
        FileReader reader = new FileReader(fileName);
        BufferedReader bReader = new BufferedReader(reader);
        
        String line;
        ArrayList<String[]> inputLines = new ArrayList<String[]>();
        
        String firstString = "AAQFKMEIVWVSTKIVLSGLAFEKFHCWFADIWVRCKFSCVSDVMQVYKIWLQKSCRTMT" +
        "TWKRCVAIGQKEGKDYYCYMNPPFGHTTQAGGMWGCEQYMGVKIQWIQTYASVGIFQPSA" +
        "MFRISRERVFWMVDQIVPRDDNVLDKDKRDAKKWSAIMKMSSTQHVDPVRIKTYEFFVKR" +
        "KEMICPCQNAYEDFTNPHLPGKGTQPKPCKALNVVGKKNVCYKEHSCNCCAWLGWGFCEQ" +
        "NWHKPDNKSVNGLRYTFEQSWRTWHSLCQWDHRAWMMYFVKYHVHQKPQPNWECQMQFCW" +
        "ELIDHVRNWIPQTCPMGLNMKWPMMRMQKCSITKYNSRRQEGMWPHTRVLWETFFINYHS" +
        "REIVSQGSFNCKEDGVSQHGMTGGLVGCCFLHHPFDNIEEQSWDRSLPVKWTWMQDSVHG" +
        "GQVFRIATENLECQCIFHAAYVGDVKGYNMDWYESINDFGKRDTIHAVVNTWDNMANNQM" +
        "RNWFDMPCICPVEYDCAYIRQNIHVCMAPEYFCRVGQVAAYRSWKREWASGLDQYHAYVM" +
        "PKLWNMEDCQEMPQWAPIVYCGKALFDELFSVEGCKIGFRIGTWDMIIAGIQLYHKMIVD" +
        "TLMPHGACRGGHLEMVVGPIWPPSMYMTIAWNLRHCMNNHWASQSEAMATFGDDLGSMQL" +
        "GLWHFTQRVIKPIKCRVMDARVAGSDECISEWIPVDCSFHLPWTWQRFRPTVESKTWHYF" +
        "RNTHWMDECDSHLKNQWYKYWHKGSEVFKYHANGAHLW";
        String secondString = "AAQTKIVLWGLKIYYWVADIWVRCKFSVQVDKIWLQKSYSVAYTNLSADICAIGQKEGKD" +
        "YTWYCYMNPPFKHTTQFEGMWGCWQYMGVKITPVPMVWIQTYASQPSAHFRISRERVFGM" +
        "SDQIVPRWDNVLDKDKRDAQKWSAMKMSSTQQPDFFVKRKSYLKHGDKSICPFQNAYEDF" +
        "INNMLHGKHTDACTPHRLCMGDPLQETFAQKPCKALNVVGKKNVCYKPYGVCHPPGWGFC" +
        "EQMWHKADNKSVNWGNWDTTFLRYTFLPCDDWDHRAWIMYFVKYHVHECQMQFYEAFNGW" +
        "EWELIDPQTCPQRFMFWEKMWYKTWMTVVRMDTKTNGRRQEGTHPHTRVLHKTGTFGVRE" +
        "IKEDGTCQHGMTGGLVGCCFLHHPFFNTEEQSWDRSLQDSVHGGQVMRMASENLECQCIF" +
        "HAAYVGDVKIFFIGYNMDWYESPNDFGKRDHAQYNLSRNYMENAYSLQDDMPWICPNEYD" +
        "CAYQYTTELQLPRQNIHVCMVNFTMPRPEYFALMRLPLRFAGEGNRSWMPAVLEREWASG" +
        "LDQYHNNTLIMTYVMNMEDCQEMPQWAPIVYCGKALFYHKPDNPQELEIPWPFSVEGCKQ" +
        "GFRIGLYKPSITAYRHKMIVDTLMPHGACRGGHLEMTGHYRVGGLVIWKPLMYMTIPWNL" +
        "RHCMNNFWASQSELQMYTFGCCRTCLWHWFTQRNKPINCRVMDARVAKAHSDECISEVDC" +
        "SFMLESRVHAWTWQRFRPGTARQLRWMDEYGSHLKNQWQHKMMIDSWCIWCQPHIW";
        
        while( (line = bReader.readLine()) != null){
            inputLines.add(line.split(" "));
        }
        
        int[][] scoringMatrix = new int[20][20];
        int[][] output;
        int counter = 0;
        for( String[] d:inputLines){
            for( int i = 0; i < 20; i++){
                scoringMatrix[counter][i] = Integer.parseInt(d[i]);
            }
            counter++;
        }
        
        output = GlobalAligner(firstString, secondString, scoringMatrix);
        //OutputLCS(output, firstString.length(), secondString.length(), firstString, secondString);
        //System.out.println(scoringMatrix[0][0]);
        
    }
    
    public static int[][] GlobalAligner(String string1, String string2, int[][] matrix){
        int indelPenalty = 5;
        Map<String, Integer> matrixMap = new HashMap<String, Integer>();
        String[] aminoAcids = {"A", "C", "D", "E", "F", "G", "H", "I", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "Y"};
        
        int[][] Backtrack = new int[string1.length()+1][string2.length()+1];
        
        for( int i = 0; i < aminoAcids.length; i++){
            matrixMap.put(aminoAcids[i], i);
        }
        
        int[][] s = new int[string1.length() + 1][string2.length() + 1];
        
        for( int i = 0; i <= string1.length(); i++ ){
            s[i][0] = i * -indelPenalty;
        }
        for( int j = 0; j <= string2.length(); j++){
            s[0][j] = j * -indelPenalty;
        }
        
        for( int i = 1; i <= string1.length(); i++ ){
            for( int j = 1; j <= string2.length(); j++){
                s[i][j] = Math.max(s[i-1][j-1] + matrix[matrixMap.get(Character.toString(string1.charAt(i-1)))][matrixMap.get(Character.toString(string2.charAt(j-1)))],
                    Math.max(s[i][j-1] - indelPenalty,s[i-1][j] - indelPenalty));
                
                if( s[i][j] == s[i-1][j] - indelPenalty){
                    //deletion "-"
                    Backtrack[i][j] = 0;
                }
                else if( s[i][j] == s[i-1][j] - indelPenalty){
                    //insertion Character.toString(string2.charAt(j-1))
                    Backtrack[i][j] = 1;
                }
                else if( s[i][j] == s[i-1][j-1] + matrix[matrixMap.get(Character.toString(string1.charAt(i-1)))][matrixMap.get(Character.toString(string2.charAt(j-1)))]){
                    Backtrack[i][j] = 2;
                }
            }
        }
        
        System.out.println(s[string1.length()][string2.length()]);
        //System.out.println(string1);
        return Backtrack;
    }
    
    public static void OutputLCS(int[][] Backtrack, int i, int j, String string1, String string2){
        if( i == 0 || j == 0){
            return;
        }
        
        if(Backtrack[i][j] == 0){
            OutputLCS(Backtrack, i-1, j, string1, string2);
            System.out.print("-");
        }
        else if(Backtrack[i][j] == 1){
            OutputLCS(Backtrack, i, j-1, string1, string2);
            System.out.print(string2.charAt(j-1));
        }
        else if(Backtrack[i][j] == 2){
            OutputLCS(Backtrack, i-1,j-1, string1, string2);
            System.out.print(string2.charAt(j-1));
        }
    }
}
