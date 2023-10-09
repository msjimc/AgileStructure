# AgileStructure user guide

## Data requirements

### Aligned data format
AgileStructure is designed to visualise aligned long read data formatted as `*.bam` files with their related index file. To aid the importing of data, it is expected that the index file will have the same name as the bam file with the `*.bam.bai` extension appended to the bam files name, for instance the bam file:  
```CNTNAP2.srt.mm2.bam```  
while have a index file named:  
```CNTNAP2.srt.mm2.bam.bai```  

### Preferred long read sequence aligners

Long reads that span a break point will appear to consist of two regions of homology, mapping to different locations in the genome. How these chimeric alignments are reported are aligner specific. Some aligners such as minimap2 ([github](https://github.com/lh3/minimap2), [paper](https://academic.oup.com/bioinformatics/article/34/18/3094/4994778)), treat the two regions as different alignments, but will report the secondary alignment as a CIGAR string in the alignments tag section, while those like lra ([github](https://github.com/ChaissonLab/LRA), [paper](https://journals.plos.org/ploscompbiol/article?id=10.1371/journal.pcbi.1009078)), will report a single alignment with an insertion or deletion reported in the alignments CIGAR string.  
AgileStructure is able to analyse both annotations, but the first method is the most flexible and will allow more complex break points to be processed than those that don't report the location of secondary alignments. Consequently, it is recommended to align data using an aligner like minimap2. 

### Optional data

To aid the analysis, it is possible to view the putative break points with reference to the  location of repeat and genes sequences. This data can be obtain from the USCS genome browser as described [here](downloadingOptionalFiles.md).

## Importing alignment data

