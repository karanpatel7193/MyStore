import { Component } from '@angular/core';
import { SliderComponent } from "../slider/slider.component";
import { BlockComponent } from './block/block.component';
import { BannerComponent } from '../banner/banner.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [SliderComponent, BlockComponent, BannerComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

}
