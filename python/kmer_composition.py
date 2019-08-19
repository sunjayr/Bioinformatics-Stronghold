from __future__ import print_function
from pprint import pprint

def create_kmer(root, chars_left, alphabet, kmers):
    if chars_left == 1:
        return kmers
    
    else:
        for letter in alphabet:
            new_root = root + letter
            kmers.add(new_root)
            result = create_kmer(new_root, chars_left - 1, alphabet, kmers)
            kmers.update(result)
        return kmers

def create_kmers(kmer_length, alphabet):
    all_kmers = []
    for letter in alphabet:
        result = create_kmer(letter, kmer_length, alphabet, set())
        all_kmers.extend(list(result))
    return all_kmers

def parse_fasta(file_lines):
    sequence = ""
    for line in file_lines:
        if line[0] == '>':
            continue
        else:
            sequence += line
    return sequence

if __name__ == "__main__":
    with open('./input/kmer_comp.fa') as f:
        lines = f.read().split('\n')
    kmer_length = 4
    alphabet = ['A', 'T', 'C', 'G']
    
    sequence = parse_fasta(lines)
    all_result = create_kmers(kmer_length, alphabet)
    final_result = sorted(list(filter(lambda x: len(x) == kmer_length, all_result)))
    initial_count = [0] * len(final_result)
    kmer_counts = dict(zip(final_result, initial_count))
    for i in range(len(sequence) - 3):
        kmer = sequence[i:i+4]
        kmer_counts[kmer] += 1
    values = []
    for kmer in final_result:
        values.append(kmer_counts[kmer])
    values = map(str, values)
    with open('kmer_output.txt', 'w') as f:
        f.write(' '.join(values) + '\n')
    