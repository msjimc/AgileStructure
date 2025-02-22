# AgileStructure user guide

- [Installation](#installation)
  * [Windows](#windows-computers)
  * [Linux, BSD and macOS](#linux-macos-and-bsd)
- [Data requirements](#data-requirements)
  * [Prior knowledge of the likely location of the breakpoint](#prior-knowledge-of-the-likely-location-of-the-break-point)
  * [Aligned data format](#aligned-data-format)
  * [Preferred long read sequence aligners](#preferred-long-read-sequence-aligners)
  * [Optional data](#optional-data)
- [Importing alignment data](#importing-alignment-data)
  * [How indexed BAM files are processed](#how-indexed-bam-files-are-processed)
- [Selecting the region to view](#selecting-the-region-to-view)
- [Hiding reads without a soft-clipped segment](#hiding-reads-without-a-soft-clipped-segment)
- [Looking for putative breakpoints in the selected region.](#looking-for-putative-break-points-in-the-selected-region)
- [Viewing read alignment information](#viewing-read-alignment-information)
- [Selecting reads linked to a breakpoint](#selecting-reads-linked-to-a-break-point)
- [Saving alignment information for selected reads](#saving-alignment-information-for-selected-reads)
- [Annotating breakpoints using soft-clipped data](#annotating-breakpoints-using-soft-clipped-data)
  * [Deletion](#deletion)
  * [Duplication](#duplication)
  * [Insertion](#insertion)
  * [Inversion](#inversion)
  * [Translocation](#translocation)
  * [Complex rearrangements](#complex-rearrangements-indels)
  * [Analysis of complex rearrangements using synthetic read datasets ](#analysis-of-synthetic-read-datasets)
- [Identifying Indels using the primary alignments CIGAR string](#identifying-indels-using-the-primary-alignments-cigar-string)
  * [Important note](#important-note)
  * [Identifying insertions using the primary alignments CIGAR string](#identifying-insertions-using-the-primary-alignments-cigar-string)
  * [Identifying deletions using the primary alignments CIGAR string](#identifying-deletions-using-the-primary-alignments-cigar-string)
- [Navigating the read data](#navigating-the-read-data)
  * [Changing the region by typing the coordinates](#changing-the-region-by-typing-the-coordinates)
  * [Moving the region to the left and right with the left and right arrow keys](#moving-the-region-to-the-left-and-right-with-the-left-and-right-arrow-keys)
  * [Changing the width of the region with the Up and Down arrow keys](#changing-the-width-of-the-region-with-the-up-and-down-arrow-keys)
  * [Changing the region by selecting a region with the mouse](#changing-the-region-by-selecting-a-region-with-the-mouse)
  * [Changing the regions using the History menu options](#changing-the-regions-using-the-history-menu-options)
- [Selecting an area that contains a specific gene](#selecting-an-area-that-contains-a-specific-gene)
- [Viewing data with reference to genomic features](#viewing-data-with-reference-to-genomic-features)
  * [Displaying gene positions](#displaying-gene-positions)
  * [Displaying repeat positions](#displaying-repeat-positions)
- [Images](#images)
  * [Save alignment display images](#save-alignment-display-images)
  * [Draw schematic diagram of alignment patterns](#draw-a-schematic-diagram-of-alignment-patterns)
- [Miscellaneous functions](#miscellaneous-functions)
  * [Cursor location](#cursor-location)
  * [Aligner string](#aligner-string)
  * [Reset height of a panel](#resizing-the-height-of-the-panels)

<small><i><a href='http://ecotrust-canada.github.io/markdown-toc/'>Table of contents generated with markdown-toc</a></i></small>

## Installation

### Windows computers
```AgileStructure``` can be run simply by downloading AgileStructure.exe, AgileStructure.dll, AgileStructure.deps.json, and AgileStructure.runtimeconfig.json files from either the [64-bit](../program/AgileStructure_64/) or [32-bit](../program/AgileStructure_32/) folders to the same folder and then double-clicking on the AgileStructure.exe file. The program requires the .NET 6.0 (or later) framework to be installed, which can be obtained from here: [version 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) or [version 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0), and selecting the appropriate ***.NET Desktop Runtime \*.\*.\****  

### Linux, macOS and BSD

Like on Windows computers, AgileStructure doesn't require installation (just download the [4 files](../program/) to the same folder), but it does require ___Wine___ to be installed on the computer. A number of short guides covering the installation of ___Wine___ on approximately 20 versions of Linux, BSD, and macOS can be found [here](https://github.com/msjimc/RunningWindowsProgramsOnLinux).  ```AgileStructure``` also requires the installation of the .NET frameworks, usually achieved via ___Winetricks___.

## Data requirements

### Prior knowledge of the likely location of the breakpoint

 ```AgileStructure``` is designed to identify breakpoints with user assistance rather than scan the whole alignment for possible breakpoints; consequently, it is expected that the user will have some prior knowledge as to where the breakpoint is, such as a cytogenetics and/or karyotype report, a list of known disease genes for the patient's condition, or a single pathogenic variant in a patient with a recessive disease for whom a second pathogenic variant cannot be found. 

### Aligned data format

```AgileStructure``` is designed to visualise aligned, long-read data formatted as indexed ```BAM``` files. It's expected that the index file will have the same name as the ```BAM``` file with the ```*.bai``` extension appended to the BAM file's name. For instance, the BAM file:  
```CNTNAP2.srt.mm2.bam```  
will have an index file named:  
```CNTNAP2.srt.mm2.bam.bai```  
which will be in the same directory as the ```BAM``` file.  
The ```BAM``` file must contain the header section, which lists the name and size of each reference sequence in the reference genome.

### Preferred long-read sequence aligners

Long reads that span a breakpoint will appear to consist of two regions of homology that map to different locations in the genome. How these chimeric alignments are reported is aligner-specific. Some aligners, such as Minimap2 ([GitHub](https://github.com/lh3/minimap2), [paper](https://academic.oup.com/bioinformatics/article/34/18/3094/4994778)), treat the two regions as different alignments but will report the secondary alignment as a condensed CIGAR string in the primary alignment's tag section, while other aligners may report the read as two separate alignments that are not directly referenced in the other alignment's position and CIGAR string. However, for shorter indels, both types of aligners may report them in the CIGAR string ([see this section](#identifying-indels-using-the-primary-alignments-cigar-string)).  
```AgileStructure``` is only able to analyse reads in which the indel is reported in the primary alignment's CIGAR string or the secondary alignment is reported in the tag section. The reporting of the secondary alignment in the tag section is the most flexible method and will allow more complex breakpoints to be processed. Consequently, it is recommended to align data using an aligner such as minimap2. 

### Optional data

To aid the analysis, it is possible to view the putative breakpoints with reference to the location of genes and repetitive sequences. This data can be obtained from the USCS genome browser as described [here](downloadingOptionalFiles.md).

## Importing alignment data

Data is imported as a pair of files, the pre-aligned BAM file and its index file, by either pressing the ```BAM file``` button (Figure 1a) or by selecting the ```Analysis``` > ```Open BAM file``` menu option (Figure 1b) and selecting the required ```BAM``` file.

![Figure 1a](images/figure1a.jpg)

Figure 1a

![Figure 1b](images/figure1b.jpg)

Figure 1b

```AgileStructure``` will read the header section of the BAM file and populate the dropdown list box next to the ```BAM file``` button with the names of the reference sequences in the BAM file (Figure 2).

![Figure 2](images/figure2.jpg)

Figure 2

### How indexed BAM files are processed

Before a BAM file is indexed, the reads are sorted by their genomic coordinates. The data is then compressed using the qzip algorithm to create chunks of compressed data that contain reads that begin in a small area. For example, the first chunk in a file may contain reads with alignments starting on chr 1 between 1 bp and 16,000 bp, with the next chunk containing reads whose alignment starts on chr 1 between 16,000 bp and 32,000 bp. The indexing of a file creates a 2nd file (.bam.bai) that lists the start point of each chunk in the BAM file and the start position of the first read in that chunk.   
When a program has to find reads mapping to a certain region, it looks in the bam.bai files to find the chunks that contain the start positions of the reads mapped to the region. Once it's found the chunks mapping to the data, it looks up in the index file where that data starts in the BAM file and then reads the data at that point in the BAM file until it comes to the end of the compressed chunk. It repeats the process until it has read all the data for the region of interest.  
This works fine for short-read data since all the reads in a chunk start between two well-defined points (the genomic start site of the chunk and the end of the chunk plus the length of the read, i.e., 16,000 bp to 32,000 bp plus 150 bp). However, with long read data, a read may be longer than the size of a chunk's region, so a read 20 kb long may start in  one chunk while its end may be in the next or the next but one chunk. This causes an issue when you select a small region, as reads that overlap the region may be listed in a chunk much further upstream and so may not be found when reading the BAM file.  Consequently, ```AgileStructure``` reads the chunk that ends just before the start of the region of interest, but if a read is particularly long, it may be missed as it starts even further upstream. Therefore, when selecting regions, initially select a slightly larger (16 kb added to each side) region than required and note if a long read spanning the breakpoint disappears when you shrink the region.      

## Selecting the region to view

Select the required chromosome (reference sequence) from the upper dropdown list box and enter the region's coordinates in the two text boxes to the right of this dropdown list box and press the ```Get reads``` button (Figure 3). The coordinates are first checked to make sure they are not greater than the chromosome's length as reported in the ```BAM``` file. If no chromosome has been selected, these values will be limited to '1'. If the region is valid, reads aligned in the region will be displayed in the upper panel.   

![Figure 3](images/figure3.jpg)

Figure 3

The positions of reads mapping to the region are depicted as green rectangles (aligned to the forward strand) and red rectangles (aligned to the reverse strand), scaled according to the length of the read. Soft-clipped sequences are represented as pale green or pale red extensions adjacent to the darker green/red-aligned data. The size of these pale rectangles is proportional to the length of the unaligned sequence, and their placement merely indicates whether they are on the 5’ or 3’ end of the aligned sequence.  
It is important to note that in the default view, reads are represented as a solid box spanning the length of the alignment; if a read has a large deletion, this will not be shown; however, they can be visualised by selecting the ```Analysis``` > ```Look for indels within a read``` menu option (see section [Identifying Indels using the primary alignments CIGAR string](#identifying-indels-using-the-primary-alignments-cigar-string)).  
```AgileStructure``` does not have an upper limit on the size of the region or number of reads it will process and will attempt to read the requested data until it has processed the region or the computer runs out of memory. While there is no upper limit, you should try to limit the amount of data that is read because reading the underlying BAM file can be a slow process due to its size.

## Hiding reads without a soft-clipped segment

When adding reads to the display, they are stacked to use as little space as possible. However, for alignments with a high read depth, the stacks may be too tall to fit in the image. Since reads that have a soft-clipped region are more important in breakpoint detection, it is possible to hide those without an unaligned fragment by selecting the ```Analysis``` > ```Only show reads with secondary alignments ``` menu option (Figure 4).

![Figure 4](images/figure4.jpg)

Figure 4

The filtered image will contain fewer reads, making those at the breakpoint more apparent.

![Figure 5](images/figure5.jpg)

Figure 5: When reads with no secondary alignment are removed, the breakpoint is more apparent: see the mouse cursor in Figure 5 and compare it to the same region in Figure 3

## Looking for putative breakpoints in the selected region.

It may be possible to simply identify the breakpoint at this point, especially for large homozygous deletions, but in many situations, particularly for heterozygous breakpoints, they may not stand out. Consequently, ```AgileStructure``` scans the displayed reads, looking for 250 bp regions in which multiple read alignments prematurely terminate and the remaining soft-clipped sequences all map to the same secondary location. These regions are then noted and entered into the lower dropdown list box (Figure 6).

![Figure 6](images/figure6.jpg)

Figure 6

For extended regions and/or alignments with a high read depth, this list may contain a large number of entries. To filter these regions, press the ```Filter``` button to the left of the lower dropdown list box. This will open the ```Filter possible breakpoints``` window (Figures 7a and 7b). The upper dropdown list box allows the breakpoints to be filtered by the chromosome that the secondary alignments are mapped to (Figure 7a), while the lower number select box will filter the results by the number of reads linked to each putative breakpoint. 

![Figure 7a](images/figure7a.jpg)

Figure 7a

![Figure 7b](images/figure7b.jpg)

Figure 7b

Pressing the ```Accept``` button will remove all breakpoints that do not match the criteria (Figure 8), while pressing the ```Cancel``` button will remove all filtering.

![Figure 8](images/figure8.jpg)

Figure 8

Selecting a breakpoint from this list will cause the reads with soft-clipped sequences mapped to the second breakpoint’s flanking regions to be displayed in the lower panel (Figure 9). As before, reads are represented in green or red for those mapping to the forward and reverse strands, with aligned sequences darker than the unaligned soft-clipped sequences. It is important to note that only reads that are present in the upper image are shown in the lower image, and that sequences that were aligned to the reference sequence in the upper image will be unaligned, soft-clipped sequences in the lower image.

![Figure 9](images/figure9.jpg)

Figure 9

## Viewing read alignment information

Selecting the ```Data``` > ```View read data``` (Figure 10a) will cause a resizable window to appear that consists solely of a text area. If the mouse cursor is held over a read, its underlying data will be written to the text area (Figure 10b). For a sequence to be shown, the cursor has to hover over the read for a little while. This makes it possible to select a read, then quickly move the cursor to the new window and copy the data to paste into a document.

![Figure 10a](images/figure10a.jpg)

Figure 10a

![Figure 10b](images/figure10b.jpg)

Figure 10b

Since the read, quality score string, and CIGAR string can be several thousand characters long, the text area doesn’t word wrap text (unless it’s very long), and so if you want to read the end of a CIGAR string, you must use the horizontal scroll bar.
In addition to the sequence and quality string, this information contains the primary and secondary alignment locations, as well as all the tags added by the aligner. The format of the tag data can be aligner-specific, with the aligner’s documentation providing a full description of each tag’s meaning.

## Selecting reads linked to a breakpoint

When the upper image contains a large number of reads, it may not be possible to identify the reads associated with the selected breakpoint. However, clicking on a read in either panel's image will cause it to be selected and outlined with a blue border. Clicking on all the reads linked to a breakpoint in the lower image will help to identify the location of the breakpoint in the upper image (Figure 11).

![Figure 11](images/figure11.jpg)

Figure 11

If you click on a selected read, it will be deselected, while selecting the ```Data``` > ```Clear selected reads``` option will deselect all selected reads (Figure 12). Finally, if new data is imported from the BAM file (i.e., the ```Get reads``` button is pressed), the selection will be cleared.

![Figure 12](images/figure12.jpg)

Figure 12

## Saving alignment information for selected reads

Rather than manually saving the data for a series of read alignments, it's possible to save the data of selected reads to a text file using the ```Data``` > ```Save selected reads``` (Figure 13).

![Figure 13](images/figure13.jpg)

Figure 13

## Annotating breakpoints using soft-clipped data

Once the reads spanning a breakpoint have been selected, it is possible to get ```AgileStructure``` to attempt to identify the type of variant: deletion, duplication, insertion, inversion, or translocation. To identify what type of mutation the breakpoint represents, select the ```Variant determination``` > ```Use soft-clip data``` > ```Variant type``` menu option (Figure 14a). ```AgileStructure``` will then scan the orientation of the primary and secondary alignments of the selected reads to determine what type of mutation it is. This is reported in a message box with the possible answers of “Deletion”, “Insertion”, “Inversion”, “Duplication” or “Translocation” as well as messages indicating any error processing the data or user data selection issues (Figure 14b).  
***For this feature to work, a region must be selected in the lower panel***.

![Figure 14a](images/figure14a.jpg)

Figure 14a

![Figure 14ba](images/figure14b.jpg)

Figure 14b

Once the variant type is determined, it is then possible to annotate the breakpoint by selecting the appropriate option. The links below show examples of analysing each type of mutation.


### Deletion

A worked example is [here](deletion.md).

### Duplication

A worked example is [here](duplication.md).

### Insertion

A worked example is [here](insertion.md).

### Inversion

A worked example is [here](inversion.md).

### Translocation

 A worked example is [here](translocation.md).

 ### Complex rearrangements (indels)

A worked example is [here](complex.md).

## Analysis of synthetic read datasets

The links above describe the analysis of real data using the functions for simple rearrangements. Since these only touch on a fraction of the possible rearrangement scenarios, the analysis of a large number of different rearrangements is available [here](../synthetic/README.md). These analyses use synthetic read data to create the required variant read data and allow the annotated result to be compared to the known variant.

[Synthetic test data representing complex rearrangements](../synthetic/README.md)

## Identifying indels using the primary alignment's CIGAR string  

```AgileStructure``` is primarily designed to identify chromosomal breakpoints by looking for sets of reads whose alignment is broken in two, such that their primary alignment aligns at one location and their secondary alignments are all located to a more distant common region, possibly on a different chromosome. However, it is also able to identify insertions and deletions that do not cause the alignment to be fragmented but whose presence is noted in the primary alignment's CIGAR string.  
Selecting the ```Analysis``` > ```Look for indels within a read``` menu option (Figure 15) causes the reads to be redrawn with deletions shown as a horizontal black line linking two blocks of aligned sequences while an insertion is shown as a vertical line projecting above and below the aligned sequence. Since ONT data contains numerous short indels, only insertions/deletions longer than 10 bp are shown.  
The data can be filtered for reads with large insertions and deletions within them by the ```Analysis``` > ```Only show reads with an indel``` menu option.

![Figure 15](images/figureA.jpg)

Figure 15

When redrawn using the CIGAR string to identify insertions and deletions, their presence becomes apparent. For example, in Figure 16, the large 39 kb deletion spanning 1,495,000 bp to 1,534,000 bp of chromosome 1 and the insertion at 1,586,000 bp (above the cursor) are easily identified as a series of broken read alignments linked by a pale grey line.

![Figure 16](images/figureB.jpg)

Figure 16

### Important note

Since ONT data is very noisy, the exact point of the breakpoint may appear to vary by a number of base pairs between different reads, while artefactual indels may also be present in the reads. Consequently, ```AgileStructure``` scans the beginning and end of each indel, sorts them by position, and then reports the median values for the reported variant. Using the median value rather than the average reduces the chance an artifactual indel will unduly influence the annotation, but it is important to ensure that the individual indels are checked to make sure a 2nd, possibly artifactual, indel is not somehow disrupting the annotation.

### Identifying insertions using the primary alignment's CIGAR string

To annotate an insertion, select the reads containing the variant of interest, then navigate to the ```Variant determination``` > ```Use primary alignment's CIGAR string``` > ```Insertion``` menu option (see Figure 17a). A message box will appear, listing any insertions larger than 10 bp along with the names of the selected reads and the variant's annotations. 
***Note:*** Reads with multiple insertions may need to be deselected for accurate analysis. 

![Figure 17a](images/figureCa.jpg)

Figure 17a

![Figure 17b](images/figureCb.jpg)

Figure 17b: The order of the reads in the message box is determined by chromosomal position, which may differ from the displayed order of the reads.

### Identifying deletions using the primary alignment's CIGAR string  

To annotate a deletion, select the reads containing the variant of interest and then select the  ```Variant determination``` > ```Use primary alignment's CIGAR string``` > ```Deletion``` menu option (Figure 18a). This will open a message box, listing the deletions over 10 bp, followed by the names of the reads and finally, the variant's annotation (Figure 18b).

![Figure 18a](images/figureDa.jpg)

Figure 18

![Figure 18b](images/figureDb.jpg)

Figure 18b: The reads in the message box are ordered by chromosomal position, which may differ from their display order.

## Navigating the read data

### Changing the region by typing the coordinates

As noted earlier, ```AgileStructure``` presents the primary and secondary alignments in separate panels. At the top of each panel, there are text fields for adjusting the start and end points of the displayed data. Retrieval of primary read data from the BAM file can be time-consuming, so updates to the primary alignment region occur after the user clicks the ```Get reads``` button.  In contrast, modifications to the secondary alignment coordinates are implemented immediately.  

### Moving the region to the left and right with the left and right arrow keys

Instead of manually entering new locations into the text areas, you can easily shift the region to the left or right. Simply click on one of the text areas to activate it (i.e., you could edit the value by typing), and then press the ```Ctrl``` + ```left arrow``` or ```Ctrl``` + ```right arrow``` keys. This will move the region in the desired direction to an adjacent, non-overlapping region of the same length as the original.

### Changing the width of the region with the Up and Down arrow keys

In a similar manner to moving the region to the left or right, you can double or half the width of the region. Simply activate a text area and then press the ```Ctrl``` + ```Up arrow``` or ```Ctrl``` + ```Down arrow``` keys. Even though the size of the region changes, it remains centered on the same point in the reference sequence, unless the new region extends beyond the end of the chromosome, in which case the region is modified accordingly.

### Changing the region by selecting a region with the mouse

A sub-region within each panel can be selected by clicking at the desired start point and dragging the cursor to the end point with the right mouse button held down (refer to Figure 19a). After releasing the button, the display updates to show the selected area (see Figure 19b). This feature enables a detailed examination of specific regions; for example, Figure 19a shows four reads with a seemingly common insertion. However, closer inspection in Figure 19b reveals discrepancies in their locations, indicating they may be artifacts. To investigate further, selecting ```Variant determination``` > ```Use primary alignment's CIGAR string``` > ```Insertion``` menu option reveals that all reads share a 134 to 135 bp insertion. This uniformity suggests that the insertion’s position might be incorrect, potentially due to sequencing errors or alignment issues with low-complexity sequences (illustrated in Figure 19c).

![Figure 19a](images/figureEa.jpg)

Figure 19a: 

![Figure 19b](images/figureEb.jpg)

Figure 19b

![Figure 19c](images/figureEc.jpg)

Figure 19c

### Changing the regions using the History menu options

Whenever adjustments are made to the primary or secondary display coordinates, the previous positions are automatically stored. This feature enables users to revisit earlier views by choosing the saved coordinates from the ```History``` dropdown menu, under ```Primary alignments``` or ```Secondary alignments``` (as shown in Figure 20).

![Figure 20](images/figureF.jpg)

Figure 20

## Selecting an area that contains a specific gene

The [Displaying gene positions](#displaying-gene-positions) section explains how to import gene locations; once imported, it is possible to navigate to a region that contains a specific gene by selecting the ```Annotation``` > ```Gene coordinates``` menu option (Figure 21a).

![Figure 21a](images/figureLa.jpg)

Figure 21a

Activating this option will launch the ```Gene coordinates``` window, which features two text fields. Enter the gene symbol of interest into the top text field (refer to Figure 21b).  

![Figure 21b](images/figureLb.jpg)

Figure 21b

Click the ```Find``` button, and if the gene symbol is present in the imported gene coordinate data, its coordinates will be displayed in the lower text field (Figure 21c).

![Figure 21c](images/figureLc.jpg)

Figure 21c

Pressing ```Accept``` will reset the coordinates in the ```AgileStructure``` main window to the gene's location, and pressing the ```Get reads``` button will update the Primary alignment window (Figure 21d).

![Figure 21d](images/figureLd.jpg)

Figure 21d

## Viewing data with reference to genomic features

It is possible to visualise the read data in relation to the genes and repeats around the breakpoint. The chromosomal coordinates of the genes and repeats can be downloaded from the UCSC Genome Browser as outlined in this [guide](downloadingOptionalFiles.md). 

### Displaying gene positions

Gene coordinate data is imported by selecting the ```Annotation``` > ```Gene annotation file``` menu option (Figure 22).

![Figure 22](images/figureG.jpg)

Figure 22

Genes are displayed as black rectangles, with their exons depicted by green (gene on forward strand) or yellow (gene on reverse strand) positioned at the bottom of the displays (Figure 23). 

![Figure 23](images/figureH.jpg)

Figure 23

Clicking on a gene will cause its name to be displayed to the top left of the appropriate display. For instance, in Figure 24, the gene near the breakpoint on chromosome 4 (primary alignment) is BLOC154, and on chromosome 22 (secondary alignment) it's SHANK3.

![Figure 24](images/figureI.jpg)

Figure 24

### Displaying repeat positions

Repeat coordinates are imported by selecting the ```Annotation``` > ```Select repeat annotation file``` option (Figure 25). Unlike the gene positions, repeats are only drawn when the ```Annotation``` > ```Show repeats``` option is selected (Figure 25). This is due to the large number of repeats,  which require substantial memory to store and can be slow to render across wide regions. Consequently, ```AgileStructure``` will only retain the repeat file's filename and reload the required data from the file each time the repeats need to be drawn.

![Figure 25](images/figureJ.jpg)

Figure 25

The repeats are drawn as black rectangles filled in pale blue (forward strand) or pale yellow (reverse strand) across a single row at the very bottom of the displays. As with genes, clicking on a repeat will cause its name, class, and family to be displayed at the top left of the display. For example, in Figure 26, the repeat close to the breakpoint on chromosome 4 (primary alignment) is labelled as MLT1F1-int, LTR, ERVL-MalR AluSz, SINE, Alu, while at the chromosome 22 breakpoint (secondary alignment) it's labelled as (GGCCCG)n, Simple_repeat, Simple_repeat.

![Figure 26K](images/figureK.jpg)

Figure 26

## Images

### Save alignment display images

While ```AgileStructure```'s display is primarily concerned with read selection, they can be saved as images, which could be used in a report or lab book. To save the display pictures, select the ```image``` > ```Save alignment images``` menu item (Figure 27a). This will prompt you for the image's file name and location before saving it (Figure 27b).

![Figure 27a](images/figureOa.jpg)

Figure 27a

![Figure 27b](images/figureOb.jpg)

Figure 27b: The resultant image is a direct copy of the current displays.

### Draw a schematic diagram of alignment patterns

The organisation of each read's primary and secondary alignments can be difficult to discern. Consequently, ```AgileStructure``` is able to create a schematic representation of selected reads that span a rearrangement's breakpoints. All rearrangements annotated by ```AgileStructure``` consist of 2, 3, or 4 columns of split read alignments; therefore, the schematic diagram is typically drawn as 4 evenly spaced positions (not drawn to scale) that represent each of the columns. While the diagram isn't drawn to scale, if two or more columns are very close to each other, their positions will be drawn closer together.   

To draw a schematic diagram, select the ```Image``` > ```Draw schematic image``` menu option, which should open the ```Draw schematic display``` window (Figure 28a).

![Figure 28](images/figurePa.jpg)

Figure 28

While the ```Draw schematic display``` window is open, it is still possible to interact with the main window, allowing you to select the split-reads of interest from one of the columns of split-reads and then press the upper ```Accept``` button on the ```Draw schematic display``` window. This will clear the selection and draw the reads in the lower panel of the ```Draw schematic display``` window (Figure 29).

![Figure 29](images/figurePb.jpg)

Figure 29

The schematic diagram consists of four labels across the top of it that represent the four possible columns of split-read alignments. To the right of the labels is a fifth column that indicates the number of split reads that demonstrate the alignment pattern drawn below the labels.

The alignment is drawn as two rectangles; if both rectangles are green, both alignments are in the same orientation. If one is red and the other green, this indicates the orientation is different between the two alignments. 

#### Adding a second breakpoint

In this case the rearrangement is a translocation, and by selecting the ```Variant determination``` > ```Switch region``` menu option of the main window, you can view the complementary breakpoint. After selecting a region for the secondary alignments in the lower panel of the main window and then selecting the relevant split-read alignments, you can add these to the schematic diagram by pressing the lower ```Accept``` button (Figure 30). If split-reads from both breakpoints have the same alignment pattern and base pair location, the reads are combined; however, if the locations differ at all, they will be displayed as two independent features. 

![Figure 30](images/figurePc.jpg)

Figure 30

The split-reads from both breakpoints suggest the same location on chromosome 22 (50,721,690 or 50,721,691); however, the chromosome 4 split-reads suggest two breakpoints slightly more distant at 6,715,708 and 6,715,726. It is possible that this is due to issues aligning the reads to the genome or is result of a genuine lose of sequence.

#### Linking split read columns

In figure 30, the locations on chromosome 4 are seen as two distinct positions, as they are 18 base pairs apart; however, by increasing the value in the numeric field below the lower ```Accept``` button, the two positions will be drawn closer together (Figure 31). Even though the columns are linked, the labeling is unchanged.

![Figure 31](images/figurePd.jpg)

Figure 31

## Miscellaneous functions

### Changing a read's selection colour

When a read has been selected, by default it is highlighted by a blue border. However, when saving an image of the split-read alignments, it may be desirable to change the colour code of the split-reads. This can be done by selecting the ```Image``` > ```Colour code selected reads``` menu option. This will open the ```Colour selection``` window (Figure 33).

![Figure 32](images/figureQa.jpg)

Figure 32

To colour code the split reads, first select the reads you wish to be the same colour and then press the button on the window that is the desired colour (Figure 33). In Figure 33, the split-reads with primary alignments mapping to the 5 prime side of a duplication have been selected, and the ```black``` button on the ```Colour selection``` window pressed. The alignments are now highlighted with black borders. 

![Figure 33](images/figureQb.jpg)

Figure 33

It can be seen that a selected split read has two secondary alignments in the selected region. The ```Clear``` button was pressed to deselect the reads, and the split read with two secondary alignments was selected, and the ```Dark Green``` button was pressed (Figure 34).  

![Figure 34](images/figureQc.jpg)

Figure 34

Finally, the ```Colour selection``` window was closed, and all the split reads spanning the breakpoint were selected. Those with primary alignments at the 5' end of the duplication have a black border, while those on the 3' side have a blue border. The read with two secondary alignments in the region is identifiable, as it has a green border.

![Figure 35](images/figureQd.jpg)

Figure 35

The colours are selected as they are spread across the spectrum. However, pressing the ```Custom```  button allows a user-defined colour to be selected using the colour selection dialogue box (Figure 35).


![Figure 36](images/figureQe.jpg)

Figure 36

### Cursor location

The ```Annotation``` > ```Show position of cursor``` menu option reveals the genomic coordinates of the cursor's position (Figures 37a and 37b). It’s important to note that this representation is approximate. For instance, a 1 Mb region depicted on an 860-pixel-wide image results in approximately 1,162.8 bps per pixel. Therefore, this feature should be used as a general guide to understand the region rather than a precise tool.

![Figure 37a](images/figureNa.jpg)

Figure 37a

![Figure 37b](images/figureNb.jpg)

Figure 37b

### Aligner string

Typically, the aligner used to map reads to the reference genome includes the command line arguments in the BAM file’s header section. This information can be accessed by selecting the ```Data``` > ```Aligner string``` menu option (see Figures 38a and 38b). This feature is particularly useful when, for instance, you need to verify which reference genome was used for the alignment.

![Figure 38a](images/figureMa.jpg)

Figure 38a

![Figure 38b](images/figureMb.jpg)

Figure 38b: The command string identifies that the data in the '16.03193_Rpt.fastq.gz' file was aligned to the 'ucsc.hg19.fasta' reference genome by Minimap2 using the map-ont alignment options.

### Resizing the height of the panels

When first opened, ```AgileStructure``` draws each display panel the same size; however, the upper primary alignment panels may display more reads than the lower panel, and so ideally it is drawn taller than the lower panel. In Figure 39a, the full height of the upper panel is used to display primary alignments, with some omitted as there is insufficient space to draw them. If the cursor is placed below the panel, the mouse point will change from an arrow to a horizontal bar with two arrows pointing up and down (black box in Figure 39a). If the left mouse button is pressed and the cursor moved down (Figure 39b), the upper panel is drawn taller at the expense of the lower panel (Figure 39c).

[Figure 39a](images/figureRa.jpg)

Figure 39a

![Figure 39b](images/figureRb.jpg)

Figure 39b

![Figure 39c](images/figureRc.jpg)

Figure 39c
