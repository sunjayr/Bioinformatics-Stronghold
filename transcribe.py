def transcribe2(string):
    return string.replace('T', 'U')

def transcribe(string):
    rna = []
    for letter in string:
        if letter == 'T':
            rna.append('U')
        else:
            rna.append(letter)
    return ''.join(rna)

print transcribe2("GATGGAACTTGACTACGTAAATT")
