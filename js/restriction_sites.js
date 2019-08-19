const fs = require('fs');

function readFasta() {
    let fastaObj = {};
    fs.readFile('./input/restriction_sites_input.txt', (err, data) => {
        for (let line of data.toString().split('\n')) {
            if (line.startsWith('>')) {

            }
        }
    });
}

function generateKmers(kmerLength) {
    kmerList = [];
}

function main(){
    readFasta();
}

main();