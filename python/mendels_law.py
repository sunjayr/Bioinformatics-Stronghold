def calculate(k,m,n):
    summation = [k * (k - 1),
                 k * m,
                 k * n,
                 m * k,
                 m * (m - 1) * 0.75,
                 m * n * 0.5,
                 n * k,
                 n * m * 0.5]
    total = k + m + n
    probability = sum(summation) * (1/total) * (1 / (total - 1))
    return probability

with open('mendel_input.txt', 'r') as f:
    nums = map(float, f.read().split(' '))
    k = nums[0]
    m = nums[1]
    n = nums[2]
print calculate(k,m,n)


