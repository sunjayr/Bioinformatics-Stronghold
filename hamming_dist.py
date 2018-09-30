def hamming_distance(string1, string2):
    if len(string1) != len(string2):
        print "Cannot compute Hamming Distance"
        return
    dist = 0
    for i in range(0,len(string1)):
        if string1[i] != string2[i]:
            dist += 1
    return dist

print hamming_distance("GCTCGGGGGGAGACCGATTGACTAAACGTACTCGGGCGATAAAAGATATAGTTCACAAAGGCACCGAGCTTTGCTGGCCTTGACGATTTCTCGCTAGCAGACCAAAACCGGGCTACAGACTGTTATGCCCATGTCTTTCAGCGACGCAGAGTACAGCCTTATGCTTGAGAACCGAGCAAATCGTCTATCTGGTATTTTGCATATAACACGAGTTAATCACTACCACGGAACTTCGGATACTTAAGTGAGTTGTGGGGTACCTCGATCAGCTCCGTTTGTCACGGCCACAAGTAACGACTCTCGTCGCCCGGTGGGCCTCATCAGGCACAGATAGACCCTTCCGTCCAGTCAAGTAACTAACCAGAAAGCCGACCGGAAGTCATCTATGGTCAGCAAACGTCGATGCCATATAGCACTTTGGAACGATGCTTAGAGAATAGTGGCAAAAGAATACGCCATCCTAACGGAAGCATATGGCTACAAGACGTGTATTCGTGCGATTAAACTAATTGCTGCAAGCACGACCTAATATCATTGATCTGTATAGCGGGATTCATAACCAGAACGTGGAGGGGACACTCATCTACATCACACGTTGAGCGGCTCGATGTGGACAAGCTCCCCGTATTCCCGCGTAAACTATGGCGCTAGGTTTGTTTCCAACTCTTCGGTCCTTCCAGTCTAAGAGAACCGGTACGCACCTACCAGCTTATGACTCTTGATGGTCTATAGTTGGCTCCAAGAAGTAAACGTTTAGACGCCCGAGAGAGTATGTGATTGGTTACGGAAGGGTTGAGGTCTGATCACCAGTACTAGATGAGCTTGCACAATTAATAAGCAAGGTACCACGATTGGTGCCTAAACCTGTATCCTGTGGATAGTTTACAGTCTAAAGTACACGGTGCGCTAGCTGGCTACGACATGTTAAATATGAATAGCA","GGGCATGGCCGGGCCACGCGACTTTGACGTCTGAGGCGACACGTTGGTAACTTAGCATTCGCGCCGAGCAAATATGCCGCACGCGAACACTATGTCGGGAACCAAAAACCGACTGCTGTCATCTTTCTCAAGAACTGTAGAAGCCGAAGGGACTCAGCATAGGAATGCGAGCCGTGTATACCTTCTATACGTGACTTAGCTGATGAGTCGATTAATCGACGCCGTCTGAAAAGGAATACCTCCAAGATGCCATTAGCTACAACGAACAGATCCAGGTGGCACCGGCTCGTAAGACACACTAGGTCGGGCGGGGTGCGACTCCCTCGGCACACCCACCGTGTCGCCGTGTAGATTAATTAACTCTACAGGTCGAGATACGACTACTTCGCACATATCCCGGCGATGTAATATTGCATCTGGGAACGGTTGCCTGCGAATCGTGAGAAGATTACATTCCGATCGCCCTGAGGAATCTGGCAAAAAGAGGCGCAGTCATAGGAATTGCTCACTGCAGTCAAGCGGTAGCCCATATGCGTGCTCGACCGAGGGCGATACATTGTCAGAACGTGAGAGCTACCAGATTCTAATTGATTAGTTGAAGAGATCGGTGCTGACAAGCTCCGGACGTTGGCTCTTGAACTGTGATGCTACGGTCGTTAACTTTCCCTCGGTTCATAGAGTCAGGGTGATCCTGCACCCTCCTGTCGGTTCAGCAATACTGTTGGTCTACAGTCGGCACCAACATGTTCTAGTCGAGAAGCCCAACTGGGCATGAGGAGGGGAACAGCGGTGTATGGACGAGAACACTACTTCCATAGTTTACTACACATAGAAGGGCGTAGCTCACACGATTGCTGCTTGAGCTCATGTAATAACCATGCTTTTAGGGCGAGAGACCCCTATAAGTCCGCGACGTCTGTGTTGTGATGTATGAACAATA")