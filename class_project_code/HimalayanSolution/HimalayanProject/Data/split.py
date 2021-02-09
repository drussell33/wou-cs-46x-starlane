# split large file into files with 300 lines each with right extension
# {prefix} + {number} + . + {ext}

main = open('seed.sql', 'r') 
nthFile = 1
child_file = 0
count = 0
for line in main:
    if count % 300 == 0:
        child = "seed" + str(nthFile) + ".sql"
        child_file = open(child, 'w')
        nthFile+=1
    
    count += 1
    child_file.writelines(line)

main.close() 
child_file.close()

