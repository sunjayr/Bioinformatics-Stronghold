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
public class MultipleAlignments {
    
    public static void main( String [] args ){
        String string1 = "ATATCCG";
        String string2 = "TCCGA";
        String string3 = "ATGTACTG";
        
        Alignments(string1, string2, string3);
        
        int[][][] output;
    }
    
    public static int[][][] Alignments(String v, String w, String u){
        int[][][] s = new int[v.length() + 1][w.length() + 1][u.length() + 1];
        int[][][] Backtrack = new int[v.length()+1][w.length()+1][u.length()+1];
        
        for( int i = 0; i < s.length; i++){
            s[i][0][0] = 0;
            Backtrack[i][0][0] = 0;
        }
        
        for( int j = 0; j < s[0].length; j++){
            s[0][j][0] = 0;
        }
        
        for( int k = 0; k < s[0][0].length; k++){
            s[0][0][k] = 0;
        }
        
        for( int i = 1; i <= v.length(); i++){
            for( int j = 1; j <= w.length(); j++){
                for( int k = 1; k < u.length(); k++){
                    if( v.charAt(i-1) == w.charAt(j-1) && v.charAt(i-1) == u.charAt(k-1) &&
                            w.charAt(j-1) == u.charAt(k-1) ){
                        System.out.println("Entered");
                        s[i][j][k] = Math.max(s[i-1][j-1][k-1] + 1,
                                 Math.max(s[i-1][j][k-1],
                                 Math.max(s[i-1][j-1][k], 
                                 Math.max(s[i-1][j][k], 
                                 Math.max(s[i][j-1][k-1],
                                 Math.max(s[i][j-1][k], s[i][j][k-1]))))));
                    }
                    else{
                        s[i][j][k] = Math.max(s[i-1][j-1][k-1],
                                 Math.max(s[i-1][j][k-1],
                                 Math.max(s[i-1][j-1][k], 
                                 Math.max(s[i-1][j][k], 
                                 Math.max(s[i][j-1][k-1],
                                 Math.max(s[i][j-1][k], s[i][j][k-1]))))));
                    }
                    
                }
            }
        }
        
        System.out.println(s[v.length()][w.length()][u.length()]);
        return Backtrack;
    }
    
    
}
