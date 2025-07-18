import { Component, ViewChild, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbCarouselModule, NgbCarousel, NgbSlideEvent, NgbSlideEventSource, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SliderService } from './slider.service';
import { DomSanitizer } from '@angular/platform-browser';
import { BlockModel } from '../home/block/block.model';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
	selector: 'app-slider',
	standalone: true,
	imports: [NgbCarouselModule, FormsModule, CommonModule,RouterModule],
	templateUrl: './slider.component.html',
	styleUrls: ['./slider.component.scss']
})
export class SliderComponent implements OnInit {
	banners: BlockModel[] = [];

	constructor(private sliderService: SliderService, private sanitizer: DomSanitizer) { }

	ngOnInit(): void {
		this.loadBanners();
	}
	loadBanners(): void {
		this.sliderService.getForSlider().subscribe((response: any) => {
			// Sanitize the content before binding
			this.banners = response.data.map((banner: any) => ({
				...banner,
				content: this.sanitizer.bypassSecurityTrustHtml(banner.content)
			}));
		});
	}
}
