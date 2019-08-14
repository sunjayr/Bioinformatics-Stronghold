def recurrence(n, m):
    #m = 3
    array = [1,1]
    for i in range(2,n):
        value = array[i-1] + array[i-2]
        if i >= m:
            if i == m:
                value -= 1
            else:
                value -= array[i - (m + 1)]
    
        array.append(value)
    print array[n-1]
recurrence(94,19)