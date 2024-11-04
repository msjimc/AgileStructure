# Synthetic test data

While structural rearrangements are generally classed as **translocations**, **duplications**, **insertions**, **inversions** and **deletions**, many are more complex with for example sequence been lost at the insertion site creating an **indel**. Consequently, to test AgileStructure a number of synthetic long read data sets based  modified chromosomes (human chr 7 and 8) were  made. Since these where artificial the exact rearrangement was known checking the results is straight forward. 

Since the range of synthetic data was primarily intended to check the annotation functions, some datasets are very similar and designed to test how positions in different numeric and alphabetical orders were processed some may be redundant. However, they have been included to show what different rearrangements look like in the interface and show how to select the relevant split reads.    

Also, sine the naming convention can result in arbitrary decisions been made when annotating a variant, for instance it is not clear how close an inserted sequence can be to the original site before is not referenced to as an insertion and becomes a duplication, the variants are given a narrative description than a annotation.

The read data sets were created using [MakeSVgenome](https://github.com/msjimc/MakeSVGenome) with reads of 10 kb length created each 1kb, with the orientation set as forward, reverse, forward, reverse, forward, reverse, etc. Where sequence data was moved from one chromosome to another, reads from source chromosome were also included.   

|File|Description|Was the inserted sequence inverted|
|-|-|-|
|[balanced_translocation_p_to_p.md](balanced_translocation_p_to_p.md)|Balanced translocation|NO|
|[balanced_translocation_p_to_q.md](balanced_translocation_p_to_q.md)|Balanced translocation|Yes|
|[balanced_translocation_q_to_p.md](balanced_translocation_q_to_p.md)|Balanced translocation|Yes|
|[balanced_translocation_q_to_q.md](balanced_translocation_q_to_q.md)|Balanced translocation|NO|
|[chr7_1-3,000,000_inserted_into_chr8_1-4,000,000.md](chr7_1-3,000,000_inserted_into_chr8_1-4,000,000.md)|Unbalanced translocation|NO|
|[chr7_43,600,000-43,750,000_inserted_into_chr7_43,590,000.md](chr7_43,600,000-43,750,000_inserted_into_chr7_43,590,000.md)|Insertion near original sequence |NO|
|[chr7_43,600,000-43,750,000_inserted_into_chr7_43,590,000-43,600,000.md](chr7_43,600,000-43,750,000_inserted_into_chr7_43,590,000-43,600,000.md)|Insertion near original sequence with deletion at insertion site - Duplication|NO|
|[chr7_43,600,000-43,750,000_inserted_into_chr7_43,600,000.md](chr7_43,600,000-43,750,000_inserted_into_chr7_43,600,000.md)|Dupication (simple)|NO|
|[chr7_43,600,000-43,750,000_inserted_into_chr7_43,600,000-43,610,000.md](chr7_43,600,000-43,750,000_inserted_into_chr7_43,600,000-43,610,000.md)|Insertion with in original sequence with deletion at insertion site - Duplication|NO|
|[chr7_43,600,000-43,750,000_inserted_into_chr7_43,610,000.md](chr7_43,600,000-43,750,000_inserted_into_chr7_43,610,000.md)|Insertion within original sequence - Duplication|NO|
|[chr7_43,600,000-43,750,000_inserted_into_chr7_43,740,000.md](chr7_43,600,000-43,750,000_inserted_into_chr7_43,740,000.md)|Insertion within original sequence - Duplication|NO|
|[chr7_43,600,000-43,750,000_inserted_into_chr7_43,740,000-43,750,000.md](chr7_43,600,000-43,750,000_inserted_into_chr7_43,740,000-43,750,000.md)|Insertion with in original sequence with deletion at insertion site - Duplication|NO|
|[chr7_43,600,000-43,750,000_inserted_into_chr7_43,750,000.md](chr7_43,600,000-43,750,000_inserted_into_chr7_43,750,000.md)|Duplication (simple)|NO|
|[chr7_43,600,000-43,750,000_inserted_into_chr7_43,750,000-43,760,000.md](chr7_43,600,000-43,750,000_inserted_into_chr7_43,750,000-43,760,000.md)|Insertion near original sequence with deletion at insertion site - Duplication|NO|
|[chr7_43,600,000-43,750,000_inserted_into_chr7_43,760,000.md](chr7_43,600,000-43,750,000_inserted_into_chr7_43,760,000.md)|Insertion near original sequence |NO|
|[chr7_43,600,000-43,750,000_inserted_into_chr7_20000000-21,000,000.md](chr7_43,600,000-43,750,000_inserted_into_chr7_20000000-21,000,000.md)|Insertion in same chromosome with deletion|NO|
|[chr7_43,600,000-43,750,000_inserted_into_chr7_50000000-51,000,000.md](chr7_43,600,000-43,750,000_inserted_into_chr7_50000000-51,000,000.md)|Insertion in same chromosome with deletion|NO|
|[chr7_43,600,000-43,750,000_inserted_into_chr8_43,600,000-43,750,000.md](chr7_43,600,000-43,750,000_inserted_into_chr8_43,600,000-43,750,000.md)|Insertion on different chromosome|NO|
|[chr7_43,600,000-43,750,000_inserted_into_chr8_20000000-21,000,000.md](chr7_43,600,000-43,750,000_inserted_into_chr8_20000000-21,000,000.md)|Insertion on different chromosome|NO|
|[chr7_43,600,000-43,750,000_inserted_into_chr8_50000000-51,000,000.md](chr7_43,600,000-43,750,000_inserted_into_chr8_50000000-51,000,000.md)|Insertion on different chromosome|NO|
|[chr7_43,600,000-43,750,000_inserted_into_chr8_50000000-51000000.md](chr7_43,600,000-43,750,000_inserted_into_chr8_50000000-51000000.md)|Insertion on different chromosome|NO|
|[chr7_156,345,972-159,345,972_inserted_into_chr8_141,138,635-145,138,635.md](chr7_156,345,972-159,345,972_inserted_into_chr8_141,138,635-145,138,635.md)|Unbalanced translocation|NO|
|[chr8_1-4,000,000_inserted_into_chr7_1-3,000,000.md](chr8_1-4,000,000_inserted_into_chr7_1-3,000,000.md)|Unbalanced translocation|NO|
|[chr8_43,600,000-43,750,000_inserted_into_chr7_43,600,000-43,750,000.md](chr8_43,600,000-43,750,000_inserted_into_chr7_43,600,000-43,750,000.md)|Insertion on different chromosome|NO|
|[chr8_43,600,000-43,750,000_inserted_into_chr7_20000000-21,000,000.md](chr8_43,600,000-43,750,000_inserted_into_chr7_20000000-21,000,000.md)|Insertion on different chromosome|NO|
|[chr8_43,600,000-43,750,000_inserted_into_chr7_50000000-51,000,000.md](chr8_43,600,000-43,750,000_inserted_into_chr7_50000000-51,000,000.md)|Insertion on different chromosome|NO|
|[chr8_141,138,635-145,138,635_inserted_into_chr7_156,345,972-159,345,972.md](chr8_141,138,635-145,138,635_inserted_into_chr7_156,345,972-159,345,972.md)|Unbalanced translocation|NO|
|[RC_chr8_43,600,000-43,750,000_inserted_into_chr7_50000000_51000000.md](RC_chr8_43,600,000-43,750,000_inserted_into_chr7_50000000_51000000.md)|Insertion on different chromosome|Yes|
|[RC_of_chr7_1-3,000,000_inserted_into_chr7_56,000,000.md](RC_of_chr7_1-3,000,000_inserted_into_chr7_56,000,000.md)|Unbalanced translocation|Yes|
|[RC_of_chr7_1-3,000,000_inserted_into_chr8_141,138,635-145,138,635.md](RC_of_chr7_1-3,000,000_inserted_into_chr8_141,138,635-145,138,635.md)|Unbalanced translocation|Yes|
|[RC_of_chr7_43,600,000-43,750,000_inserted_into_chr7_43,550,000-43,800,000.md](RC_of_chr7_43,600,000-43,750,000_inserted_into_chr7_43,550,000-43,800,000.md)|Inversion with deletion|Yes|
|[RC_of_chr7_43,600,000-43,750,000_inserted_into_chr7_43,590,000-43,600,000.md](RC_of_chr7_43,600,000-43,750,000_inserted_into_chr7_43,590,000-43,600,000.md)|Insertion with in original sequence with deletion at insertion site - Inverted Duplication|Yes|
|[RC_of_chr7_43,600,000-43,750,000_inserted_into_chr7_43,600,000-43,610,000.md](RC_of_chr7_43,600,000-43,750,000_inserted_into_chr7_43,600,000-43,610,000.md)|Insertion near original sequence with deletion at insertion site - Inverted Duplication|Yes|
|[RC_of_chr7_43,600,000-43,750,000_inserted_into_chr7_43,610,000.md](RC_of_chr7_43,600,000-43,750,000_inserted_into_chr7_43,610,000.md)|Insertion near original sequence  - Inverted Duplication|Yes|
|[RC_of_chr7_43,600,000-43,750,000_inserted_into_chr7_43,650,000-43,800,000.md](RC_of_chr7_43,600,000-43,750,000_inserted_into_chr7_43,650,000-43,800,000.md)|Inversion with deletion of sequence aon 3 prime side and duplication of sequence on 5 prime|Yes|
|[RC_of_chr7_43,600,000-43,750,000_inserted_into_chr7_43,740,000.md](RC_of_chr7_43,600,000-43,750,000_inserted_into_chr7_43,740,000.md)|Insertion of reverse complement within original sequence - Duplication|Yes|
|[RC_of_chr7_43,600,000-43,750,000_inserted_into_chr7_43,760,000.md](RC_of_chr7_43,600,000-43,750,000_inserted_into_chr7_43,760,000.md)|Insertion of reverse complement near original sequence - Duplication|Yes|
|[RC_of_chr7_43,600,000-43,750,000_inserted_into_chr7_20000000-21000000.md](RC_of_chr7_43,600,000-43,750,000_inserted_into_chr7_20000000-21000000.md)|Insertion of reverse complement  on different chromosome|Yes|
|[RC_of_chr7_43,600,000-43,750,000_inserted_into_chr7_50000000-51000000.md](RC_of_chr7_43,600,000-43,750,000_inserted_into_chr7_50000000-51000000.md)|Insertion of reverse complement  on different chromosome|Yes|
|[RC_of_chr7_43,600,000-43,750,000_inserted_into_chr8_43,600,000-43,750,000.md](RC_of_chr7_43,600,000-43,750,000_inserted_into_chr8_43,600,000-43,750,000.md)|Insertion of reverse complement  on different chromosome|Yes|
|[RC_of_chr7_43,600,000-43,750,000_inserted_into_chr8_20000000-21000000.md](RC_of_chr7_43,600,000-43,750,000_inserted_into_chr8_20000000-21000000.md)|Insertion of reverse complement  on different chromosome|Yes|
|[RC_of_chr7_43,600,000-43,750,000_inserted_into_chr8_50000000-51000000.md](RC_of_chr7_43,600,000-43,750,000_inserted_into_chr8_50000000-51000000.md)|Insertion of reverse complement  on different chromosome|Yes|
|[RC_of_chr7_156,345,972-159,345,972_inserted_into_chr8_1-4,000,000.md](RC_of_chr7_156,345,972-159,345,972_inserted_into_chr8_1-4,000,000.md)|Unbalanced translocation|Yes|
|[RC_of_chr8_1-4,000,000_inserted_into_chr7_156,345,972-159,345,972.md](RC_of_chr8_1-4,000,000_inserted_into_chr7_156,345,972-159,345,972.md)|Unbalanced translocation|Yes|
|[RC_of_chr8_43,600,000-43,750,000_inserted_into_chr7_43,600,000-43,750,000.md](RC_of_chr8_43,600,000-43,750,000_inserted_into_chr7_43,600,000-43,750,000.md)|Insertion on different chromosome|Yes|
|[RC_of_chr8_43,600,000-43,750,000_inserted_into_chr7_20000000-21000000.md](RC_of_chr8_43,600,000-43,750,000_inserted_into_chr7_20000000-21000000.md)|Insertion on different chromosome|Yes|
|[RC_of_chr8_43,600,000-43,750,000_inserted_into_chr7_50000000-51000000.md](RC_of_chr8_43,600,000-43,750,000_inserted_into_chr7_50000000-51000000.md)|Insertion on different chromosome|Yes|
|[RC_of_chr8_141,138,635-145,138,635_inserted_into_chr7_1-3,000,000.md](RC_of_chr8_141,138,635-145,138,635_inserted_into_chr7_1-3,000,000.md)|Unbalanced translocation|Yes|
