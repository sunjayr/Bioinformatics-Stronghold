const fs = require('fs');

function readFasta() {
    let data = fs.readFileSync('./input/restriction_sites_input.txt');
    let sequence = data.toString().split('\n').filter(x => !x.startsWith('>'));
    return sequence.join('');
    
}

function generateKmers(kmerLength, sequence) {
    let kmerMap = {};
    for (let i = 0; i < sequence.length - kmerLength + 1; i++) {
        let k = sequence.substring(i,i+kmerLength);
        if (kmerMap.hasOwnProperty(k)) {
            kmerMap[k].push(i + 1);
        }
        else {
            kmerMap[k] = [i + 1];        
        }
    }
    return kmerMap;
    
}

function isRestrictionSite(kmer) {
    let translate_map = {
        'A':'T',
        'C':'G',
        'T':'A',
        'G':'C'
    };
    let sequence = [];
    for (let base of kmer) {
        sequence.push(translate_map[base]);
    }
    let complement = sequence.reverse().join('')
    return kmer === complement ? true : false;
}

function locateRestrictionSites(kmers) {
    for (let k in kmers) {
        if(!isRestrictionSite(k)){
            delete kmers[k];
        }    
    }
    return kmers;
}

function outputPositions(kmerMap) {
    let position_list = [];
    for (let k in kmerMap) {
        for (let pos of kmerMap[k]) {
            position_list.push([pos, k.length]);
        }
    }
    let sorted_list = position_list.sort((x,y) => {
        return x[0] >= y[0];
    });
    for (let p of sorted_list) {
        console.log(`${p[0]} ${p[1]}`)
    }
}

function main(){
    let fastaSeq = readFasta();
    let allKmers = {};
    for (let i = 4; i <= 12; i++) 
    {
        allKmers = Object.assign(allKmers, generateKmers(i, fastaSeq));        
    }
    let filteredKmers = locateRestrictionSites(allKmers);
    outputPositions(filteredKmers);
    
}

main();