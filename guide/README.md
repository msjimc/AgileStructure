# AgileStructure user guide

## Data requirements

### Prior knowledge of the likely location of the break point

AgileStructure is designed to identify break points with user input rather than scan the whole file for possible break points, consequently its expected that you have some prior knowledge as to where the break point is such has a cytogenetics and/or karyotyping report, a list of known disease genes or a single pathogenic variant in a patient with a recessive disease for whom a second pathogenic variant can not be found. 

### Aligned data format
AgileStructure is designed to visualise aligned long read data formatted as `*.bam` files with their related index file. To aid the importing of data, it is expected that the index file will have the same name as the bam file with the `*.bam.bai` extension appended to the bam files name, for instance the bam file:  
```CNTNAP2.srt.mm2.bam```  
while have a index file named:  
```CNTNAP2.srt.mm2.bam.bai```  

### Preferred long read sequence aligners

Long reads that span a break point will appear to consist of two regions of homology, mapping to different locations in the genome. How these chimeric alignments are reported are aligner specific. Some aligners such as minimap2 ([github](https://github.com/lh3/minimap2), [paper](https://academic.oup.com/bioinformatics/article/34/18/3094/4994778)), treat the two regions as different alignments, but will report the secondary alignment as a condensed CIGAR string in the alignments tag section, while those like lra ([github](https://github.com/ChaissonLab/LRA), [paper](https://journals.plos.org/ploscompbiol/article?id=10.1371/journal.pcbi.1009078)), report the read as two separate alignments, but not directly reference the other alignment position and CIGAR string. However, for shorter indels both aligns will report them in the CIGAr string (see section @@@)    
AgileStructure is able to analyse both annotations, but the first method is the most flexible and will allow more complex break points to be processed than those that hard clip the sequence and don't include the location of secondary alignment. Consequently, it is recommended to align data using an aligner like minimap2. 

### Optional data

To aid the analysis, it is possible to view the putative break points with reference to the  location of repeat and genes sequences. This data can be obtain from the USCS genome browser as described [here](downloadingOptionalFiles.md).

### Importing alignment data

Data is imported as a pair of files, the pre-aligned bam files and its index file, by either pressing the ```BAM file``` button (Figure 1a) or by selecting the ```Analysis``` > ```Open BAM file``` option (Figure 1b)  and selecting the required file.

![Figure 1a](images/figure1a.jpg)

Figure 1a

![Figure 1b](images/figure1b.jpg)

Figure 1b

AgileStructure will read the header section of the bam file and populate the dropdown list box with the name of the reference sequences in the bam file (Figure 2).

![Figure 2](images/figure2.jpg)

Figure 2

If AgileStructure appears to do nothing, it may because it is unable to find the bamreaderdll.dll file: this must be in the same folder as AgileStructure.exe.

### Selecting the region to view

Select the likely chromosome (reference sequence) from the dropdown list boz and enter the regions co-ordinates in the two text boxes to the right of the drop down list box and press the ```Get reads``` button (Figure 3).    

![Figure 3](images/figure3.jpg)

Figure 3

The position of reads mapping to the region are shown as green (aligned to the forward strand) and red (aligned to the reverse strand) rectangles scaled to the length of the read. Soft clipped sequences are identified as pale green or red extensions to the darker green/red rectangles. The size of the pale rectangles is proportionate to their length and their location only indicates whether they are on the 5' or 3' of the aligned sequence.  
It is important to note that in the default view reads are drawn as a solid box spanning the length of the alignment, if a read as a large deletion this will not be shown, however they can be visualised by selecting the ```Analsis``` > ```Look for indels within a read``` option (see section @@@@@@@@@).  
AgileStructure does not have an upper limit on the size of the region or number of reads it will process and will attempt to read the requested data until the computer runs out of memory. While there is no upper limit, you should try to limit the amount of data reads as reading the underlying bam file can be a slow process due to its size.

### Hiding reads without a soft clipped segment

When adding reads to the image, they are stacked so as little space as possible is used, however for alignments with a high read depth, the stacks may be to too tall to fit in the image. Since, only reads that have a soft clipped region are important in break point detection it is possible to hide those with out an unaligned area by selecting the ```Analysis``` > ```Only show reads with secondary alignments ``` option (Figure 4).

![Figure 4](images/figure4.jpg)

Figure 4

The filtered image will contain fewer reads, making those at the break point more apparent.

![Figure 5](images/figure5.jpg)

Figure 5

### Looking for putative break points in the selected region.

It may be possible to simply identify the the break point  at this point, especially for large homozygous deletions, but in many situation particularly for heterozygous break points they may not standout. Consequently, AgileStructure scans the reads, looking for 250 bp regions in which multiple read alignments prematurely terminate and the remaining soft clipped sequence all maps to the same location. These regions are then recorded and entered in to the lower drop down list box (Figure 6).

![Figure 6](images/figure6.jpg)

Figure 6

For extended regions and/or alignments with a high read depth, this list may contain a large number of entries. To filter them press the ```Filter``` button to the left of the lower drop down list box. This will open the ```Filter possible break points``` form (Figure 7a and 7b). The upper drop down list box allows the break points to be filtered by the chromosome that the soft clip regions are mapped too (Figure 7a), while the lower number select box will filter the results by the number of reads linked to each putative break point. 

![Figure 7a](images/figure7a.jpg)

Figure 7a

![Figure 7b](images/figure7b.jpg)

Figure 7b

Pressing the ```Accept``` button will remove all break points that do not match the criteria (Figure 8), while pressing the ```Cancel``` will remove all filtering.

![Figure 8](images/figure8.jpg)

Figure 8

Selecting a break point from this list will cause the reads with soft clipped sequences mapped to the break points flanking regions to be displayed in the lower panel (Figure 9). As before, reads are drawn in green or red for those mapping to the forward or reverse strands, with aligned sequences darker than the unaligned soft clipped sequences. It is important to note that only reads that are present in the upper image shown in the lower image and that sequences that were aligned to the reference sequence in the upper image will be unaligned, soft clipped sequences in the lower image. 

![Figure 9](images/figure9.jpg)

Figure 9

### Viewing read alignment information

Selecting the ```Data``` > ```View read data``` will cause a resizable window to appear that consist solely of a text area. If the mouse cursor is held over a read, its underlying data will be written to the text area. For a sequence to be selected, the cursor has to hover over the read for a little while. This makes it possible to select a read and then quickly move the cursor to the new window and copy the data to paste in a document etc.


![Figure 10a](images/figure10a.jpg)

Figure 10a

![Figure 10b](images/figure10b.jpg)

Figure 10b

Since the read, quality score string and CIGAR string can be several thousand characters lond, the text area doesn't word wrap text and so if you want to read the end of a CIGAR string you must use the horizontal scroll bar.   
As well as the sequence and quality string, this information contains the primary and secondary alignments' location etc as well all the tag added by the aligner.

### Selecting reads linked to a break point

When the upper image contains a large number of reads it may not be possible identify the reads associated with the selected break point, however clicking on a read in either image will cause it to be selected and drawn with a blue boarder. By clicking on all the reads linked to a break point in the lower image will help to identify the break point in the upper image (Figure 11) 

![Figure 11](images/figure11.jpg)

Figure 11

If you click on a selected read it will be deselected, while selecting the ```Data``` > ```Clear selected reads``` option will deselect all selected reads (Figure 12). Finally, iIf read data is imported from the bam file (the ```Get reads``` is pressed) the selection will be cleared.

![Figure 12](images/figure12.jpg)

Figure 12

### Saving alignment information for selected reads

Rather than manually saving the data for a series of read alignments, its possible to save all the data to a text file using the ```Data``` > ```Save selected reads``` (Figure 13).

![Figure 13](images/figure13.jpg)

Figure 13

### Annotating break points using soft clipped data

Once the reads spanning a break point have been selected, is is possible to get AgileStructure to attempt to annotate the mutation. To see what type of mutation the break point represents select ```Variant determination``` > ```Use soft clip data``` > ```Variant type``` (Figure 14a). AgileStructure will scan the reads primary and secondary alignment data and the orientation of the soft clipped data with respect to the aligned sequences to determine what type of mutation it is. This is reported in a message box with the possible answers of "Deletion", "Insertion", "Inversion", "Duplication" or "Translocation" as well as messages indicating an error processing the data or user data selection issues (Figure 14b). 

![Figure 14a](images/figure14a.jpg)

Figure 14a

![Figure 14ba](images/figure14b.jpg)

Figure 14b

Once the variant type is determined it is then possible to get AgileStructure to annotate it by selecting the appropriate option (Figure 15a and 15b)

#### Translocation

![Figure 15a](images/figure15a.jpg)

Figure 15a

![Figure 15ba](images/figure15b.jpg)

Figure 15b

The message box reporting the mutation will also allow you to save the the mutation's annotation as well as the underlying sequence data to a text file by pressing the ```Yes``` button (an example is [here](images/breakPointData.txt)).

The break point is annotated with reference to the reads selected in this case those whose primary alignment is on chromosome 4. However, the alignment will also contaion reads whose primary alignment is on the other side of the break point and whose soft clipped sequences mapped to the currently selected reference. To view these reads select the ```Variant determination``` > ```Switch region``` option , select the putative break point and reads as before (Figure 16a) and press the ```Variant determination``` > ```Use soft clip data``` > ```Translocation```. If the annotation is correct, both annotations should match with a small difference in the base pair position (Figure 16b).

![Figure 16a](images/figure16a.jpg)

Figure 16a

![Figure 15b](images/figure16b.jpg)

Figure 16b

#### Deletion 
#### Insertion
#### Inversion
#### Duplication

### Identifying Indels using the primary alignments CIGAR string  

AgileStructure was primarily designed to identify chromosomal break points by looking for sets reads whose alignment is broken up such that part of the read aligns at one specific location and the the other is located some distance away or on a different chromosome. However, it is also able to identify insertion and deletions that do cause the alignment to be fragmented, but whose presence is noted in the CIGAR string.  
Selecting the ```Analysis``` > ```Look for indels within a read``` option (Figure A) cause the reads to be redrawn with deletions shown as a horizontal black line linking to blocks os aligned sequences while an insertion is shown as a vertical line projecting above and below the aligned sequence. Since ONT data contains numerous short indels, only insertions longer than 10 are shown/processed.

![Figure A](images/figureA.jpg)

Figure A

When redrawn using the the CIGAR string to identify insertions and deletions there presence becomes apparent. For example in Figure B the large deletion spanning 1,495,000 bp to 1,534,000 bp and the insert at 1,586,000 bp (above the cursor) are easily identified.

![Figure B](images/figureB.jpg)

Figure B

#### Important note

Since ONT data is very noisy the exact point of the break point may appear to vary by a few base pase between the reads, plus artefactual indels may also be present in the reads, consequently AgileStructure scans the beginning and ends of the indels, sorts them by position and then reports the median values in the reported variant. Using the median value rather than the average reduces the chance an artifactual indel unduly influences the annotation, but it is important that the individual indels are checked to make sure a 2nd indel is not disrupting the analysis.

#### Identifying insertions using the primary alignments CIGAR string

To annotate an insert, select the reads containing variant of interest and select ```Variant determination``` > ```Use primary alignment's CIGAR string``` > ```Insertion``` (Figure Ca). This will cause a message box to appear, listing insertions over 10 bps and the read's name in the selected reads and the annotation of the variant.   

![Figure Ca](images/figureCa.jpg)

Figure C

![Figure Cb](images/figureCb.jpg)

Figure Cb

### Identifying deletions using the primary alignments CIGAR string  

To annotate a deletion, select the reads containing variant of interest and select ```Variant determination``` > ```Use primary alignment's CIGAR string``` > ```Deletion``` (Figure Da). This will cause a message box to appear, listing the deletions over 10 bps in the selected reads and the annotation of the variant.   

![Figure Da](images/figureDa.jpg)

Figure D

![Figure Db](images/figureDb.jpg)

Figure Db

### Navigating the read data

#### Changing the regions by typing in the co-ordinates

As mentions above the AgileStructure displays the primary and secondary alignments in two panels, above each are to text area where the start and end points of the displayed data can be changed. Since the primary read data is retrieved from the bam file, changes to the primary alignment image are only made when the ```Get reads``` button is pressed. However, changes to the co-ordinates of secondary alignment image are displayed instantly.  

#### Changing the regions by selecting a region with the mouse

The mouse can be used select a sub-region of the current display in either image by moving the cursor to the desired start of the new region, and the move the mouse to the end point while holding the right mouse button down (Figure Ea). When the mouse button is released the display is redrawn (Figure Eb). [In figure Eb an insert can be seen in four reads, while its position appears variable, all the reads had a 134 to 135 bp insertion suggesting its position is inaccurately placed possibly due to sequencing in the flanking sequences (Figure Ec).]

![Figure Ea](images/figureEa.jpg)

Figure E

![Figure Eb](images/figureEb.jpg)

Figure Eb

![Figure Ec](images/figureEc.jpg)

Figure Ec

#### Changing the regions using the History functions

As each change in the either primary and secondary display co-ordinates is made, the old positions are saved, allowing the views to be recreated by selecting the appropriate co-ordinates from the lists in ```History``` > ```Primary alignments``` or ```History``` > ```Secondary alignments``` (Figure F). 

![Figure F](images/figureF.jpg)

Figure F