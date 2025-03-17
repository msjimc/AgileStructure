# Exemplar data in publication
As well as the synthetic data, AgileStructure was tested an a number of experimental datasets who's analysis has been described in the [deletion](../guide/deletion.md), [duplication](../guide/duplication.md), [inversion](../guide/inversion.md) and [translocation](../guide/translocation.md) pages. The aligned long-read data and its index file for these variants is available in this folder has: AgileStructure_exemplars_hg19_mm10.bam and AgileStructure_exemplars_hg19_mm10.bam.bai file in this folder. 

The data described in the section "Insertion and deletion identification using gapped reads" of the paper and the [Identifying indels using the primary alignment's CIGAR string](../guide/README.md#identifying-deletions-using-the-primary-alignments-cigar-string) section of the user guide is present in the AgileStructure_CIGAR_lra.bam and AgileStructure_CIGAR_lra.bam.bai files.

The human data (deletion, inversion and translocation) was aligned to hg19, while the mouse data (duplication) was aligned to mm10. The regions to select for each variant are described in the table below.

## Split read data

|Variant|Chromosome|Region start|Region end|Comment|
|-|-|-|-|-|
|Deletion|chr7|146400000|146800000|Whole region|
|Inversion|chr7|27,700,000|27,800,000|Five prime arm breakpoint|
|Inversion|chr7|93,500,000|93,634,000|Three prime breakpoint|
|Translocation|chr6|167,260,000|167,305,500|Chromosome 6 breakpoint| 
|Translocation|chr8|113,670,000|113,730,000|Chromosome 8 breakpoint|
|Duplication|chr12|37,100,000|37,400,000|Whole region|

## CIGAR gapped read data
|Variant|Chromosome|Region start|Region end|Comment|
|-|-|-|-|-|
|Delection|chr1|1,430,000|1,600,000|Whole region|
|Insertion|chr1|1,430,000|1,600,000|Whole region|

