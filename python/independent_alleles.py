import scipy.special
"""
Given: Two positive integers k (k <= 7) and N (N <= 2^k). 
In this problem, we begin with Tom, who in the 0th generation has genotype Aa Bb. Tom has two children in the 1st generation,
each of whom has two children, and so on. Each organism always mates with an organism having genotype Aa Bb.

Return: The probability that at least N Aa Bb organisms will belong to the k-th generation of Tom's family tree
(don't count the Aa Bb mates at each level). Assume that Mendel's second law holds for the factors.
"""
def calculate_probability(k, n):
    num_offspring = 2 ** k
    return 1 - sum([scipy.special.binom(num_offspring, i)*(0.25 ** i) * (0.75 ** (num_offspring-i)) for i in range(0,n)])

if __name__ == "__main__":
    print(round(calculate_probability(5,7), 3))
    