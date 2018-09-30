import java.util.ArrayList;
import java.lang.Math;
/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Sunjay
 */
public class LCS {
    
    public static void main( String [] args){
        String string1 = "CGCCGCTTTCCAGATATGTCTGTCGGTGACCCCTTACCCAAGGGAGGGAGGCCCTACTTCACGTCCGCGTCGGCTGCCTCCCCTTGTACCGGGTCGATTGGTTAGAGCACTAATAACCCTGGCTGTTTCTTTCAATCAAGAAGCCTCGGCTCACTAGGTATGATAAGCGAGGCCACGCATTGTAAAATCTGATTATCCAAACACGTGGCGGGGAAGGTATACAGCCCCAGGGGATTGGCCCAATATGGCCACTTTGCCGTAGGAACGTCTAGGTGGCCGCGGATAACTATGCGACTTGCGTAGCTACAGATTACACCATCCCTGATTCCCACGGGGACAAAGCCAGGATGCAGGGCCTGGAATGAATCCGCGACTGTTTTCAGGTTCCTGCCATACTACCCGCGCGTATTCGCTGGCATGGGTCGGCTCAGCTATATAGTGGTGTCAAGGCTGTGTCAAAAATTCCACTTTGTTGGTAGCAGTTAAGCGATATGAACAACTTACATTACCCAGGACTCGTTCACGGTTTGTCATGACGTCCCAAGCTAGGGTTGAGTCAGTCCCACTTTGTACGGACCACCGACTGTTATGTATGTATTCATTAGGGTCCTAGATGGGCCTAAGGCCCATGGTAGTAAAGTAGGTTCGGTGATGGGCCCGCGCGACCACCGTTCATGTTTAATTTATATTCATCTTCATAGCCTATATCCGCATAGAGGAGCTGCAACTCGGTAAGCAGGATAGTATTGTTTCGACCGAAAGACAACGCCTCATGAGCAGCATTGTGGTATGGGAGAGTTAGACTGGCGCATGAGACCACTTTTCTCGAGCAGGACTGAGAAACTACACGACGCTTGACGTTACCTGATGTTCCTTGATCCAGACAGCCCGTCGCAGTGTATGCCCCGCGCCCAAAGCGGGATACTCACCCTGATCGCGCGCTTC";
        String string2 = "ATTGGCCGGCGCTATGGCACAGCTGGAAACCGAAGCCAATCGGAATAAAGGGGGAATCAAGGTCCGCCGCGCCAAATTTCTGAACACTTCTACTGTGCTTTTGAGCTTTAGATTCGAATTGCAACGCATCAGCTTAGCAGCACCGGCGAAGACGTCGATAATCGAGGGATTTCTAGGAGTAGAATGATATTGTGCAGTGACACCTATAACGGTAGAGTACCCGGCGTTCATAGGCGCTAGTCCCGGTCACGGCGGCTCTAGTCAACCCGCCTGCAGTGAAGATCACGGCGACGTTGGTGTGAACTATATGCGAGTCGCGCCGAAGCTTTCATTCAACTCTTCGATGCTGACACGTCCAGTCCATTACATGCAACAACCTTAGCACACAATAAGCCCCGGTGGTGACAGCGTGCCACCAGCATCGCAATGGTAGACAAGTAACTGTAAAGGGAGTCATGAAAGGTAGCCCCTTCAAGCCGTTATGTCGCTCACCCATGCCGCCTCATCGCACATGGCGCCAACGTGCTTCTCTGTGAGAGGGGCCGGCTTCTTCTGTCACGATTACCCCGTATTGCAATGGACCTGAAAATATTACCACGATTCATCCTCACGTTACGGGCGTAGCCCCCGCATCAGTCCTTCTTGCCCGTATATAGACACTCTGAAGCGGACTACCCCGACCTGGGAGATCCCGTCTACTAATTGGCTTCGCACTGACCGCGTACGCAGGAATGGCTGCACGGTGGCGCGGATCTGTAAGAATTATGCGTCATTACGAGGAGTCCTTCGCGCAGTCAGTTAGAGCGCTGACCGGAACGTATCATCTAAGGAGACTGCTATGATTAACAAAAAAAGCAATAGAGGACAGTTAATCCACGGGGTTGGTCTACAGACCCTAAGGTGAAGCCGGGCCTAGTCTAATCAACATATGCGGAGGTATTGTACCTCTAGGCGTGCCACCCCGACGACACTGTGCTTAAGATCATAGGATCTT";
        int[][] output;
        output = LCS(string1, string2);
        OutputLCS(output, string1, string1.length(), string2.length());
    }
    
    public static int[][] LCS(String string1, String string2){
        int[][] Backtrack = new int[string1.length()+1][string2.length()+1];
        int[][] s = new int[string1.length()+1][string2.length()+1];
        
        for( int x = 0; x <= string1.length(); x++){
            s[x][0] = 0;
        }
        
        for( int y = 0; y <= string2.length(); y++){
            s[0][y] = 0;
        }
        
        for( int i = 1; i <= string1.length(); i++){
            for (int j = 1; j <= string2.length(); j++){
                if(string1.charAt(i-1) == string2.charAt(j-1)){
                    s[i][j] = Math.max(s[i-1][j-1] + 1,Math.max(s[i][j-1],s[i-1][j]));
                }
                else{
                    s[i][j] = Math.max(s[i-1][j],s[i][j-1]);
                }
                
                if( s[i][j] == s[i-1][j]){
                    Backtrack[i][j] = 0;
                }
                else if( s[i][j] == s[i][j-1]){
                    Backtrack[i][j] = 1;
                }
                else if( s[i][j] == (s[i-1][j-1] + 1) && string1.charAt(i-1) == string2.charAt(j-1)){
                    Backtrack[i][j] = 2;
                }
            }
        }
        return Backtrack;
    }
             
    public static void OutputLCS(int[][] Backtrack, String v, int i, int j){
        if( i == 0 || j == 0){
            return;
        }
        
        if(Backtrack[i][j] == 0){
            OutputLCS(Backtrack, v, i-1, j);
        }
        else if(Backtrack[i][j] == 1){
            OutputLCS(Backtrack, v, i, j-1);
        }
        else if(Backtrack[i][j] == 2){
            OutputLCS(Backtrack, v, i-1,j-1);
            System.out.print(v.charAt(i-1));
        }
    }
}
