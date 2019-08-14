import itertools

n = 6
num_list = []
temp_list = []
for i in range(1,n+1):
	num_list.append(i)
perms = list(itertools.permutations(num_list))
print len(perms)
for tup in perms:
	for element in tup:
		temp_list.append(str(element))
	print ' '.join(temp_list)
	temp_list = []