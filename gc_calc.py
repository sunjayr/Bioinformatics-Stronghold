"""
Problem: Calculate the for all fasta srings in the input file and return 
the fasta id with the highest gc content with the gc content of that id.
"""

with open('gc_input.txt', 'r') as f:
    lines = f.read().split('\n')
    max_id = ''
    max_percent = 0
    string = ''
    for line in lines:
        if line[0] == '>':
            if string:
                gc_count = string.count('C') + string.count('G')
                temp_percent = float(gc_count) / len(string)
                if temp_percent > max_percent:
                    max_id = id
                    max_percent = temp_percent
            string = ''
            id = line[1:]
        else:
            string += line
    else:
        gc_count = string.count('C') + string.count('G')
        temp_percent = float(gc_count) / len(string)
        if temp_percent > max_percent:
            max_id = id
            max_percent = temp_percent
    max_percent = max_percent * 100
    print max_id
    print round(max_percent,6)