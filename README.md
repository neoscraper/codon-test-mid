
## Introduction
The genetic code is a set of rules by which DNA or mRNA is translated into proteins (amino acid sequences).

1. Three nucleotides (or tri-nucleotide), called codons, specify which amino acid will be used.
2. Since codons are defined by three nucleotides, every sequence can be read in three reading frames, depending on the starting point. The actual reading frame used for translation is determined by a start codon. 
In our case, we will define the start codon to be the most commonly used ATG (in some organisms there may be other start codons).
3. Translation begins with the start codon, which is translated as Methionine (abbreviated as 'M').
4. Translation continues until a stop codon is encountered. There are three stop codons (TAG, TGA, TAA)

## Instructions

The objective of this coding challenge is to complete the implementation for the CodonTranslator class in the codonTranslator.cs file to translate the provided codon sequences into their corresponding proteins based on the mappings provided in the data file.  

You will be required to complete the challenge for one of the included data formats (if you have time feel free to implement one of the others for extra credit).

Unit test have been provided in the CodonTranslatorTester.cs file.  Running the tests should give you an indication of the completeness of your solution. 

## Steps

1. Clone the repo:

    `git clone https://github.com/bwkennedy/codon-test-mid.git`

2. Create your own local branch using your name:

    `git checkout -b firstName_lastName`

2. Commit your changes

    `git commit -a -m 'your commit message'`

3. Create a Pull request

    `git pull-request`

### Included in this project

1. CodonTranslator.cs - This is the code file you will need to complete
2. CodonTranslatorTester.cs - This file includes completed unit tests for the CodonTranslator class. 
3. Data Files - These are the data files that designate the start and stop codones, as well as the codon/protien mappings:
    - CodonTable.csv
    - CodonTable.json
    - CodonTable.xml

Included in this project is a comma seperated value (CSV) text file with the codon translations.
Each line of the file has the codon, followed by a space, then the amino acid (or start or stop)
For example, the first line:
CTA,L
should be interpreted as: "the codon CTA is translated to the amino acid L"


### Hints

1. You should not assume that the input sequence begins with the start codon. Any nucleotides before the start codon should be ignored.
2. You should not assume that the input sequence ends with the stop codon. Any nucleotides after the stop codon should be ignored.

### Example

If the input DNA sequence is GAACAAATGCATTAATACAAAAA, the output amino acid sequence is MH.

GAACAA ATG CAT TAA TACAAAAA
        \ / \ /
         M   H
        
ATG -> START -> M
CAT -> H
TAA -> STOP