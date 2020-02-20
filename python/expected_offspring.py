"""
Given: Six nonnegative integers, each of which does not exceed 20,000. 
The integers correspond to the number of couples in a population possessing each genotype pairing for a given factor. 
In order, the six given integers represent the number of couples having the following genotypes:
AA-AA
AA-Aa
AA-aa
Aa-Aa
Aa-aa
aa-aa
Return: The expected number of offspring displaying the dominant phenotype in the next generation, 
under the assumption that every couple has exactly two offspring.
"""
import os
if __name__ == '__main__':
    script_dir = os.path.abspath(__file__)
    input_file = os.path.abspath(os.path.join(script_dir, '..', '..', 'input', 'expected_offspring_input.txt'))
    # probabilities of the dominant allele
    probability_array = [1, 1, 1, 0.75, 0.5, 0]
    with open(input_file, 'r') as f:
        gt_counts = map(int, f.read().split('\n')[0].split(' '))
        print sum([x * 2 * probability_array[i] for i,x in enumerate(gt_counts)])
