def parse_fasta_file(fasta_file):
    with open(fasta_file, 'r') as f:
        lines = f.read().split('\n')
        filtered_lines = filter(lambda x: x[0] != '>', lines)
    string = filtered_lines[0]
    filtered_lines = filtered_lines[1:]
    return string, filtered_lines

def read_codon_table(file_name):
    codon_dict = {}
    with open(file_name, 'r') as f:
        lines = f.read().split('\n')
        for line in lines:
            codon_dict[line.split(" ")[0]] = line.split(" ")[1]
    return codon_dict

def splice_introns(dna_string, substrings):
    for string in substrings:
        split_string = dna_string.split(string)
        dna_string = ''.join(split_string)
    return dna_string

def translate_exome(exome):
    codon_list = []
    rna = exome.replace('T', 'U')
    codon_dictionary = read_codon_table('RNA_table.txt')
    for i in range(0,len(rna) - 3, 3):
        current_codon = rna[i:i+3]
        codon_list.append(codon_dictionary[current_codon])

    
    return ''.join(codon_list)

if __name__ == '__main__':
    dna_string, substrings = parse_fasta_file('rna_splicing_input.txt')
    exome = splice_introns(dna_string, substrings)
    protein_string = translate_exome(exome)
    print protein_string

