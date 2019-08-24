const fs = require('fs');

function readFasta() {
    let data = fs.readFileSync('./input/restriction_sites_input.txt');
    let sequence = data.toString().split('\n').filter(x => !x.startsWith('>'));
    return sequence.join();
    
}

function generateKmers(kmerLength, sequence) {
    let kmerList = [];
    for (let i = 0; i < sequence.length - kmerLength + 1; i++) {
        kmerList.push(sequence.substring(i,i+kmerLength));
    }
    return kmerList;
    
}

function main(){
    let fastaSeq = readFasta();
    let allKmers = [];
    for (let i = 4; i <= 12; i++) 
    {
        allKmers.push(...generateKmers(i, fastaSeq));        
    }
    console.log(allKmers);
    
}

main();