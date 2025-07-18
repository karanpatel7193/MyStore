import { Component, OnInit } from '@angular/core';
import { BannerService } from './banner.service';
import { BlockModel } from '../home/block/block.model';
import { CommonModule } from '@angular/common';
import { DomSanitizer } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-banner',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './banner.component.html',
  styleUrl: './banner.component.scss',
    providers: [BannerService]
  
})
export class BannerComponent implements OnInit {
  banners: BlockModel[] = [];

  constructor(private bannerService: BannerService,private sanitizer: DomSanitizer) {}

  ngOnInit(): void {
    this.loadBanners();
  }

  loadBanners(): void {
    this.bannerService.getForBlock().subscribe((response: any) => {
      // Sanitize the content before binding
      this.banners = response.data.map((banner: any) => ({
        ...banner,
        content: this.sanitizer.bypassSecurityTrustHtml(banner.content)
      }));
    });
  }
}
