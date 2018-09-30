def recurrence(n, k):
    array = [1,1]
    for i in range(2,n):
        array.append(array[i-1] + (k*array[i-2]))
    print array[n-1]
recurrence(30,2)