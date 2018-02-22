import numpy as np

def create_string_matrix():
    with open('profile_motif_input.txt') as f:
        lines = f.read().split('\n')
        string_matrix = []
        string = ''
        for line in lines:
            if line[0] == '>':
                if string:
                    string_matrix.append(list(string))
                    string = ''
            else:
                string += line
        string_matrix.append(list(string))
        return string_matrix

def create_profile_matrix(string_mat):
    profile_matrix = {}
    string_mat = np.array(string_mat)
    
    profile_matrix['A'] = []
    profile_matrix['T'] = []
    profile_matrix['C'] = []
    profile_matrix['G'] = []
    for i in range(0,len(string_mat[0])):
        col_list = string_mat[:,i].tolist()
        
        profile_matrix['A'].append(col_list.count('A'))
        profile_matrix['C'].append(col_list.count('C'))
        profile_matrix['G'].append(col_list.count('G'))
        profile_matrix['T'].append(col_list.count('T'))
    
    return profile_matrix

def get_consensus_sequence(prof_matrix):
    n = len(prof_matrix['A'])
    consensus_seq = []
    for i in range(0,n):
        max_num = max([prof_matrix['A'][i],
                    prof_matrix['C'][i],
                    prof_matrix['G'][i],
                    prof_matrix['T'][i]])
        if max_num == prof_matrix['A'][i]:
            consensus_seq.append('A')
        elif max_num == prof_matrix['C'][i]:
            consensus_seq.append('C')
        elif max_num == prof_matrix['G'][i]:
            consensus_seq.append('G')
        elif max_num == prof_matrix['T'][i]:
            consensus_seq.append('T')
    a_list = map(str, prof_matrix['A'])
    c_list = map(str, prof_matrix['C'])
    g_list = map(str, prof_matrix['G'])
    t_list = map(str, prof_matrix['T'])

    print ''.join(consensus_seq)
    print 'A: ' + ' '.join(a_list)
    print 'C: ' + ' '.join(c_list)
    print 'G: ' + ' '.join(g_list)
    print 'T: ' + ' '.join(t_list)

result = create_string_matrix()
p_mat = create_profile_matrix(result)
get_consensus_sequence(p_mat)
