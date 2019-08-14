fasta_sequences = []
fasta_identifiers = []
fasta_dict = {}
PURINES = set(['A', 'G'])
PYRIMADINES = set(['C', 'T'])

def calculate_ratio():
    transition_count = 0
    transversion_count = 0

    if len(fasta_sequences) > 2:
        print 'ERROR: Invalid sequence input'
        SystemExit
    
    if len(fasta_sequences[0]) != len(fasta_sequences[1]):
        print 'ERROR: Invalid sequence lengths'
        SystemExit
    
    s1 = fasta_sequences[0]
    s2 = fasta_sequences[1]
    
    sequence_length = len(fasta_sequences[0])
    for i in range(0,sequence_length):
        if s1[i] != s2[i]:
            s = set(s1[i])
            t = set(s2[i])
            query_set = s.union(t)
            
            if query_set <= PURINES or query_set <= PYRIMADINES:
                transition_count += 1
            else:
                transversion_count += 1

    print float(transition_count) / float(transversion_count) 
    


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

def main():
    read_fasta_sequence('./input/transition_transversion_input.txt')
    calculate_ratio()

if __name__ == '__main__':
    main()