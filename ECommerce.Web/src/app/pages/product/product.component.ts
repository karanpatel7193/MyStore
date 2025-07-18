import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { SearchService } from '../search/search.service';
import { PropertyMainModel, PropertyModel, SearchPropertyParameterModel, SearchGridModel } from '../search/search.model';
import { ImageUrlPipe } from "../../globalization/pipe/image-url-pipe";

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [CommonModule, FormsModule, ImageUrlPipe,RouterLink],
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss'],
})
export class ProductComponent implements OnInit {
  properties: PropertyMainModel[] = [];
  values: { [key: number]: PropertyModel[] } = [];
  selectedFilters: SearchPropertyParameterModel[] = [];
  filteredProducts: any[] = [];

  private searchService = inject(SearchService);
  private route = inject(ActivatedRoute);

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const categoryId = Number(params['categoryId']);
      if (categoryId) {
        this.loadCategoryData(categoryId);
      }
    });
  }

  loadCategoryData(categoryId: number): void {
    this.searchService.getForCriteriea(categoryId).subscribe({
      next: (response: SearchGridModel) => {
        if (response) {
          this.properties = response.properties ?? [];
          this.values = this.groupValuesByProperty(response.values ?? []);
        }
      },
    });
  }

  private groupValuesByProperty(values: PropertyModel[]): { [key: number]: PropertyModel[] } {
    return values.reduce((acc, value) => {
      acc[value.propertyId] ??= [];
      acc[value.propertyId].push(value);
      return acc;
    }, {} as { [key: number]: PropertyModel[] });
  }

  onFilterChange(propertyId: number, propertyName: string, value: string, unit: string, event: Event): void {
    const checked = (event.target as HTMLInputElement).checked;
    let existingFilter = this.selectedFilters.find(f => f.propertyId === propertyId);
  
    const formattedValue = unit !== 'None' ? `${value} ${unit}` : value;
  
    if (checked) {
      if (existingFilter) {
        existingFilter.values.push(formattedValue);
      } else {
        this.selectedFilters.push({ propertyId, propertyName, values: [formattedValue] });
      }
    } else if (existingFilter) {
      existingFilter.values = existingFilter.values.filter(v => v !== formattedValue);
      if (existingFilter.values.length === 0) {
        this.selectedFilters = this.selectedFilters.filter(f => f.propertyId !== propertyId);
      }
    }
  }
  
  applyFilters(): void {
    const categoryId = Number(this.route.snapshot.queryParams['categoryId']);
    this.searchService.getForSearch({ categoryId, searchProperties: this.selectedFilters }).subscribe({
      next: response => (this.filteredProducts = response),
    });
  }
}
