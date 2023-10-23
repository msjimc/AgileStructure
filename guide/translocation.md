# Identifying a Translocation

__Prior information__  
Using standard cytogenetics tests an individual was found to have a translocation of the tip of chromosome 4 with the tip of chromosome 22. The breakpoint was narrowed down to around 6 to 7 Mb on chromosome 4 and 50 to 51 Mb on chromosome 22.  
(The read data in in the AgileStructure_dta_sorted.bam file.)

Import the data by pressing the ```Bam file``` button and then selected chromosome 4 from the upper dropdown list box and enter the co-ordinates for the approximate position of the break point in the two text boxes to the right of the dropdown list and import the data by selecting the ```Analysis``` > ```Only show reads with secondary alignments``` menu option (Figure 1)

![Figure 1](images/examples/figure1tran.jpg)

Figure 1

This will populate the upper panel with reads that have unaligned sequences that may span the break point. The lower dropdown list box will now contain a list of regions that more than two unaligned sequences have been mapped too. In the list is a region at 50.28 Mb on chromosome 22 which is highly likely to linked to the break point under investigation. Select the region on chromosome 22 to visulise the reads (Figure 2)

![Figure 2](images/examples/figure2tran.jpg)

Figure 2

Once displayed, it's possible to select the reads by clicking on them with the mouse to see where they are located on chromosome 4. The tree reads can be seen to be at approximately 6.7 Mb on chromosome 4. Select this region by moving the cursor to about 6.675 Mb, pressing the right mouse button and moving the mouse to about 6.725 Mb before releasing the right mouse button. This will expand the upper panel, showing the reads in greater detail. Since data has been read from the bam alignment file it is necessary to reselect the reads and the read in the lower dropdown list (Figure 3).  

![Figure 3](images/examples/figure3tran.jpg)

Figure 3

While other reads with secondary alignments have been placed near the break point, it is now evident that they down not span  the translocation break point. Selecting the ```Variant determination``` > ```Use soft clip data``` > ```Translocation``` menu option prompts AgileStructure to analyse the selected reads and annotate the break point: t(chr4;chr22)g.6,713,999:g.50,283,263) (Figure 4). Pressing the ```Yes``` button will save the data to a text file.

![Figure 4](images/examples/figure4tran.jpg)

Figure 4

To test analysis, select the ```Variant determination``` > ```Switch region``` menu option (Figure 5).  

![Figure 5](images/examples/figure5tran.jpg)

Figure 5

The co-ordinates used in the lower panel will be used to define the region viewed in the upper panel. Since data is read from the bam alignment file, it is necessary to select the region in the lower panel and any reads that span the translocation (Figure 6), before selecting the ```Variant determination``` > ```Use soft clip data``` > ```Translocation``` menu option (Figure 7). 

![Figure 6](images/examples/figure6tran.jpg)

Figure 6

![Figure 7](images/examples/figure7tran.jpg)

Figure 7

The break point identified in the 2nd analysis: t(chr4;chr22)g.6,713,998:g.50,283,262) is derived from a different set of reads and so could be considered an independent test. While the base pair position of the two break points are slightly different it should be close enough to design a diagnostic PCR test.

[Return user guide](README.md#translocation) 