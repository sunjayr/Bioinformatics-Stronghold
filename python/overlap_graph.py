from types import *
fasta_sequences = []
fasta_identifiers = []
adjacency_list = []
fasta_dict = {}

def read_fasta_sequence(fasta_file):
    global fasta_dict

    with open(fasta_file, 'r') as f:
        lines = f.read().split('\n')
        sequence_buffer = ""
        fasta_id = ""
        for line in lines:
            if line[0] == '>':
                if sequence_buffer:
                    fasta_sequences.append(sequence_buffer)
                    fasta_identifiers.append(fasta_id)
                    sequence_buffer = ""
                fasta_id = line[1:]
            else:
                sequence_buffer += line
        fasta_sequences.append(sequence_buffer)
    fasta_dict = dict(zip(fasta_sequences, fasta_identifiers))


def create_adjacency_list(k):
    assert type(k) is IntType, "K value is not an integer"
    for i in range(0,len(fasta_sequences)):
        for j in range(0,len(fasta_sequences)):
            if fasta_sequences[i] == fasta_sequences[j]:
                continue
            if fasta_sequences[i][-k:] == fasta_sequences[j][:k]:
                adjacency_list.append((fasta_dict[fasta_sequences[i]], fasta_dict[fasta_sequences[j]]))
    
    for tup in adjacency_list:
        print tup[0] + ' ' + tup[1]

def main():
    read_fasta_sequence('./input/overlap_input.txt')
    create_adjacency_list(3)

if __name__ == '__main__':
    main()
    