import numpy as np

def create_matrix_fasta(file_name):
    with open(file_name, 'r') as f:
        lines = f.read().split('\n')
        string_matrix = []
        string = ''
        for line in lines:
            if line[0] == '>':
                if string:
                    string_matrix.append(list(string))
                    string = ''
            else:
                string += line
        string_matrix.append(list(string))
        return string_matrix

def greedy_motif(dna, k, t):
    best_motifs = []
    for motif in dna:
        best_motifs.append(list(motif[:k]))
    
    print np.array(best_motifs)

def main():
    with open('greedy_motif_input.txt', 'r') as f:
        strings = []
        lines = f.read().split('\n')
        k,t = map(int,lines[0].split(" "))
        for i in range(1,len(lines)):
            strings.append(lines[i])
        greedy_motif(strings,k,t)

if __name__ == '__main__':
    main()
