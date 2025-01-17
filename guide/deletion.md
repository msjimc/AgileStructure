# Identifying a Deletion

The patient was originally described in: 
> Watson CM, Crinnion LA, Tzika A, Mills A, Coates A, Pendlebury M, Hewitt S, Harrison SM, Daly C, Roberts P, Carr IM, Sheridan EG, Bonthron DT. (2014) Diagnostic whole genome sequencing and split-read mapping for nucleotide resolution breakpoint identification in CNTNAP2 deficiency syndrome. Am J Med Genet A. 164A:2649-55. doi: 10.1002/ajmg.a.36679.

Sanger sequencing of the individual identified the variant as __chr7:146,534,699_146,611,541del__  with reference to the hg19 human reference sequence. 

__Prior information__  
Using clinical phenotypic data, an individual was thought to have a mutation in the CNTNAP2 gene.  

Import the alignment data by pressing the ```BAM file``` button and selecting the ```BAM``` file. While it's possible to determine the location of the CNTNAP2 gene from a number of sources, in this example, we'll get ```AgileStructure``` to identify the region using the RefSeq gene data set. To do this, first download the data set as described [here](downloadingOptionalFiles.md), then select the ```Annotation``` > ```Gene annotation file``` menu option and select the file (Figure 1).   

***Note***:
* The annotation file must be for the same reference genome build as the read data was aligned to. 
* Since the patient was initially described with reference to the HG19 human reference sequence, the reads were aligned to this build rather than hg38.


![Figure 1](images/examples/figure1del.jpg)

Figure 1

The file will take a couple of seconds to load before you can select the ```Annotation``` > ```Gene coordinates``` menu option, which will display the ```Gene coordinates``` window (if no bam file was selected, this window will not appear). Enter the gene's symbol (CNTNAP2) into the upper text area and press the ```Find``` button. The coordinates for CNTNAP2 will then appear in the lower text area. (Figure 2)

![Figure 2](images/examples/figure2del.jpg)

Figure 2: The coordinates for CNTNAP2 in the human genome (hg19)

Pressing the ```Accept``` button will then close the window, and the gene's coordinates will appear in the upper panel's dropdown list and text areas, while pressing the ```Get reads``` will display reads mapping to the CNTNAP2 locus. Since a RefSeq annotation file was entered, the exons of any gene in the region will be displayed (Figure 3). 

![Figure 3](images/examples/figure3del.jpg)

Figure 3

Since the individual is homozygous for the mutation, its location is easily seen as an area with no aligned read data. Consequently, it is possible to select the deletion without looking at the locations of the secondary alignments in the lower panel (Figures 4 and 5).

![Figure 4](images/examples/figure4del.jpg)

Figure 4

To view the region in more detail, place the mouse cursor just before the start of the region, press the right mouse button, and move the cursor to just after the end of the region (Figure 4) before releasing the mouse button to zoom in on the deletion (Figure 5).

![Figure 5](images/examples/figure5del.jpg)

Figure 5

While you don't need to use the lower panel to identify the deletion's breakpoints, for ```AgileStructure``` to annotate a breakpoint, it does require a region to be selected in the lower dropdown list. Selecting the ```chr7 146,611,542 bp (7)``` option from the dropdown list above the secondary alignment display will only display one side of the deletion; to view both sides, press the ```Copy region``` button, which will copy the values from the upper text areas to the lower text areas (Figure 6). This aligns the positions of the primary alignments to those of the secondary alignments. Selecting primary alignments mapping to one side of the deletion will highlight their secondary alignments at the other side of the deletion (Figure 6).

![Figure 6](images/examples/figure6del.jpg)

Figure 6

After selecting all the reads spanning the deletion, pressing the ```Variant determination``` > ```Use soft clip data``` > ```Deletion``` menu option (Figure 7) prompts ```AgileStructure``` to analyse the selected reads and annotate the breakpoint as __chr7.146,837,611_146,914,450del__ (Figure 8).  

![Figure 7](images/examples/figure7del.jpg)

Figure 7

![Figure 8](images/examples/figure8del.jpg)

Figure 8

As stated above, the variant had previously been identified as:  
 __chr7:146,534,699_146,611,541del__   
 in the hg19 reference build, and the annotation can be seen to closely match this result:

|Origin|Variant|
|-|-|
|Publication|chr7:146,534,699_146,611,541del|
|This guide|chr7.146,534,703_146,914,542del|


[Return user guide](README.md#deletion) 