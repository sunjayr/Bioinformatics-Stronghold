def parse_fasta_file(fasta_file):
    fasta_lines = []
    with open(fasta_file, 'r') as f:
        lines = f.read().split('\n')
    line_buffer = ""
    for line in lines:
        if line[0] == '>':
            if line_buffer:
                fasta_lines.append(line_buffer)
                line_buffer = ""
            continue
        else:
            line_buffer += line
    if line_buffer:
        fasta_lines.append(line_buffer)
    return fasta_lines

def findLCS(sequences):
    common_strings = []
    sequences = sorted(sequences)
    shortest_sequence = sequences[0]
    for i in range(len(shortest_sequence)):
        for j in range(len(shortest_sequence), 1, -1):
            seq = shortest_sequence[i:j]
            for string in sequences[1:]:
                if seq not in string:
                    break
            else:
                common_strings.append(seq)
    common_string = max(common_strings, key=len)
    print common_string

if __name__ == '__main__':
    lines = parse_fasta_file('shared_motif_input.txt')
    findLCS(lines)