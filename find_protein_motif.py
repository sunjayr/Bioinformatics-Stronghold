"""
Problem: Given n uniprot protein database ids identify the locations of
n-glycosylation motifs in each uniprot id. Print the uniprot id along
with the locations of the motif in the fasta sequence. 
"""
from six.moves import urllib
import re

def parse_fasta_string(fasta_contents):
    string = ""
    fasta_contents = filter(lambda x: x != '', fasta_contents)
    for line in fasta_contents:
        if line[0] == '>':
            continue
        string += line
    return string

def main():
    with open('find_protein_motif_input.txt', 'r') as f:
        protein_ids = f.read().split('\n')

    for protein in protein_ids:
        locations = []
        url = 'http://www.uniprot.org/uniprot/%s.fasta' % protein
        page_resp = urllib.request.urlopen(url)
        page = page_resp.read().split('\n')
        protein_string = parse_fasta_string(page)
        
        for i in range(0,len(protein_string) - 4):
            pattern = protein_string[i:i+4]
            if re.match("N[^P][ST][^P]", pattern):
                locations.append(i + 1)
        
        if locations:
            locations = map(str, locations)
            print protein
            print ' '.join(locations)

if __name__ == '__main__':
    main()