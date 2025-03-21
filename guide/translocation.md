# Annotation of Translocations

### The aligned data for this variant is [here](../ExemplarData/) 

This patient was first described in:
> Hu L, Liang F, Cheng D, Zhang Z, Yu G, Zha J, Wang Y, Xia Q, Yuan D, Tan Y, Wang D, Liang Y, Lin G. Location of Balanced Chromosome-Translocation Breakpoints by Long-Read Sequencing on the Oxford Nanopore Platform. Front Genet. 2020 Jan 14;10:1313. doi: 10.3389/fgene.2019.01313. PMID: 32010185; PMCID: PMC6972507.

__Prior information__  
Using standard cytogenetics tests, an individual was found to have a translocation between chromosome 6 and chromosome 8. The breakpoint was narrowed down to around 113 to 114 Mb on chromosome 8 and 167 to 168 Mb on chromosome 6.  

The read data is available from the NCBI SRA page as sample [SRR9982132](https://www.ncbi.nlm.nih.gov/sra/?term=SRR9982132). For this guide, the data was aligned to the hg19 human reference sequence using minimap2.

Import the data by pressing the ```BAM file``` button and selecting the appropriate BAM file. Then, select chromosome 8 from the upper dropdown list box and enter the coordinates for the approximate position of the breakpoint in the two text boxes to the right of the dropdown list. Import the data by selecting the ```Get reads``` button (the data on the SRA website only contains data for the region flanking the breakpoint on chromosome 8) (Figure 1).

![Figure 1](images/examples/figure1tran2.jpg)

Figure 1

This will populate the upper panel with reads that have unaligned sequences that may span the breakpoint. The lower dropdown list box will now contain a region where more than two unaligned sequences have been mapped to the same location. This region is at 167.28 Mb on chromosome 6, which is close to the suggested site of the second breakpoint. Select the chromosome 6 region to visualise the secondary alignments of the reads on chromosome 8 (Figure 2).

![Figure 2](images/examples/figure2tran2.jpg)

Figure 2

Once displayed, you can select the reads in the lower panel by clicking on them with the mouse to see where they are located on chromosome 8. The four reads can be seen to be mapped to approximately 113.7 Mb on chromosome 8 (Figure 3).

![Figure 3](images/examples/figure3tran2.jpg)

Figure 3

Selecting the ```Variant determination``` > ```Use soft clip data``` > ```Translocation``` menu option prompts ```AgileStructure``` to analyse the selected reads and annotate the breakpoint as 
__t(chr6;chr8)(g.167,281,719;g.113,696,098)__ (Figure 4). Since ```AgileStructure``` identified alignments mapping to both sides of the breakpoint, it reports the translocation is balanced but notes the number of alignments on either side of the breakpoint is skewed (in this case 4 to 1). 
Pressing the ```Yes``` button will save the data to a text file. 

![Figure 4](images/examples/figure4tran2.jpg)

Figure 4

To test the analysis, select the ```Variant determination``` > ```Switch region``` menu option (Figure 5).  

![Figure 5](images/examples/figure5tran2.jpg)

Figure 5

The coordinates used in the lower panel will now define the region viewed in the upper panel. Since data is re-read from the BAM alignment file, it is necessary to select the region in the lower panel and any reads that span the translocation (Figure 6) before selecting the ```Variant determination``` > ```Use soft clip data``` > ```Translocation``` menu option (Figure 7). 

![Figure 6](images/examples/figure6tran2.jpg)

Figure 6

![Figure 7](images/examples/figure7tran2.jpg)

Figure 7

The breakpoint identified in the 2nd analysis:  
__t(chr6;chr8)(g.167,281,716;g.113,696,100)__  
is derived from a different set of reads and could be considered an independent test. While the base pair positions of the two breakpoints are slightly different from each other (Table 1) and the published position (less than 3 bp), it should be close enough to design a diagnostic PCR test.

Since this side of the translocation has more reads spanning it, ```AgileStructure``` is able to state the translocation is balanced. It also suggests the derivative chromosomes have a p telomere and a q telomere from the original chromosomes.

|Origin|Variant|
|-|-|
|Published variant|t(chr6;chr8) (g.167,281,717:g.113,696,089)|
|Using primary alignments on chr8|t(chr6;chr8) (g.167,281,719;g.113,696,098)|
|Using primary alignments on chr6|t(chr6;chr8) (g.167,281,716;g.113,696,100)|

#### Table 1

[Return user guide](README.md#translocation) 
