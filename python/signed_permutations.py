import itertools
import copy
                
if __name__ == "__main__":
    all_permutations = []
    n = 5
    int_pairs = [[i, -1 * i] for i in range(1, n + 1)]
    signed_combos = map(list,list(itertools.product(*int_pairs)))
    for p in signed_combos:
        all_permutations += list(itertools.permutations(p))
    print len(all_permutations)
    for l in all_permutations:
        print ' '.join(map(str, l))