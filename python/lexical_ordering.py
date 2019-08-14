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

if __name__ == "__main__":
    with open('./input/lexical_input.txt', 'r') as f:
        lines = f.read().split('\n')
    alphabet = lines[0].split(' ')
    kmer_length = int(lines[1])
    all_result = create_kmers(kmer_length, alphabet)
    final_result = sorted(list(filter(lambda x: len(x) == kmer_length, all_result)))
    for kmer in final_result:
        print(kmer)