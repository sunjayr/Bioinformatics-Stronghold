import os
import json

SCRIPT_DIR = os.path.dirname(os.path.abspath(__file__))

def create_aa_codon_map():
    aa_map = {}
    for codon in codon_to_aa_map.keys():
        if codon_to_aa_map[codon] not in aa_map.keys():
            aa_map[codon_to_aa_map[codon]] = [codon]
        else:
            aa_map[codon_to_aa_map[codon]].append(codon)
    return aa_map

if __name__ == '__main__':
    with open(os.path.join(SCRIPT_DIR, '..', 'input', 'infer_mrna_input.txt'), 'r') as fi:
        protein_string = fi.read()
        protein_string += '*'
    with open(os.path.join(SCRIPT_DIR, '..', 'data', 'codon_table.txt'), 'r') as f:
        file_lines = f.read().split('\n')
    codons = map(lambda x: x.split(' ')[0], file_lines)
    amino_acids = map(lambda x: x.split(' ')[1], file_lines)
    codon_to_aa_map = dict(zip(codons, amino_acids))
    aa_to_codon_map = create_aa_codon_map()

    rna_strings = 1
    for aa in protein_string:
        rna_strings *= len(aa_to_codon_map[aa])
    print rna_strings % 1000000
