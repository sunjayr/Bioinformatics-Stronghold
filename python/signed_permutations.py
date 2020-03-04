import itertools
import copy

def permutation_length(n):
    permutation = [i for i in range(1,n+1)]
    all_permutations.extend(list(itertools.permutations(permutation)))
    fixed_index = 0
    while fixed_index < len(permutation):
        perm_copy = copy.deepcopy(permutation)
        perm_copy = map(lambda x: x * -1, perm_copy[:fixed_index]) + perm_copy[fixed_index:]
        for i in range(fixed_index, len(permutation)):
            perm_copy[i] = perm_copy[i] * -1
            all_permutations.extend(list(itertools.permutations(perm_copy)))
            perm_copy[i] = perm_copy[i] * -1
        fixed_index += 1
                
if __name__ == "__main__":
    all_permutations = []
    permutation_length(3)    
    print len(all_permutations)
    for perm in all_permutations:
        print ' '.join(map(str, list(perm)))