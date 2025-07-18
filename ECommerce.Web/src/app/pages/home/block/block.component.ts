import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { BlockService } from './block.service';
import { ImageUrlPipe } from "../../../globalization/pipe/image-url-pipe";

interface CategoryModel {
  id: number;
  name: string;
  imageUrl: string;
  parentId: number;
}

@Component({
  selector: 'app-block',
  standalone: true,
  imports: [CommonModule, RouterLink, ImageUrlPipe],
  templateUrl: './block.component.html',
  styleUrl: './block.component.scss',
  providers: [BlockService]
})
export class BlockComponent implements OnInit {
  categories: CategoryModel[] = [];
  blocks: any[] = []; // Define a variable to store blocks data
  products: any[] = [];
  constructor(private blockService: BlockService) { }

  ngOnInit(): void {
    // this.loadCategories();
    this.getBlockList();
  }

  private loadCategories(): void {
    this.blockService.getCategoryList().subscribe(response => {
      if (response?.data) {
        this.categories = response.data;
      }
    });
  }
  getBlockList(): void {
    this.blockService.getBlockList().subscribe((response: any) => {
      this.blocks = response.data.blocks;
      this.products = response.data.products; // Products from the response
    });
  }

  // Get products related to a specific block
  getProductsForBlock(blockId: number): any[] {
    return this.products.filter(product => product.blockId === blockId);
  }
  chunkProducts(products: any[], chunkSize: number): any[][] {
    const chunks = [];
    for (let i = 0; i < products.length; i += chunkSize) {
      chunks.push(products.slice(i, i + chunkSize));
    }
    return chunks;
  }
  getChunkSize(): number {
  if (window.innerWidth >= 1200) return 4; // Large screens
  if (window.innerWidth >= 992) return 3;  // Medium screens
  if (window.innerWidth >= 768) return 2;  // Small screens
  return 1; // Extra small screens (mobile)
}


}