import { CommonModule } from '@angular/common';
import { Component, HostListener, inject, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { NgbPopoverModule } from '@ng-bootstrap/ng-bootstrap';
import { CategoryModel } from '../../pages/home/category/category.model';
import { ProductModel, ProductParameterModel } from '../../pages/product/product.model';
import { CategoryService } from '../../pages/home/category/category.service';
import { WishlistService } from '../../pages/wishlist/wishlist.service';
import { AuthService } from '../../services/auth.service';
import { ImageUrlPipe } from "../../globalization/pipe/image-url-pipe";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterModule, NgbPopoverModule, ImageUrlPipe],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  public isScrolled: boolean = false;
  public navbarOpen: boolean = false;
  public isCartOpen: boolean = false;
  public isDropdownOpen: boolean = false;
  public showModal: boolean = false;
  public showProfileModal: boolean = false;
  public isUserLoggedIn: boolean = false;
  public categoryList: CategoryModel[] = [];
  public wishlistCount: number = 0;
  public selectedCategoryId: number | null = null;
  public productParameterModel: ProductParameterModel = new ProductParameterModel();

  private categoryService = inject(CategoryService);
  private wishlistService = inject(WishlistService);
  private authService = inject(AuthService); 

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.isUserLoggedIn = this.authService.IsAuthenticated(); // Check authentication on init
    this.loadCategories();
    this.wishlistService.wishlistCount$.subscribe((count) => {
      this.wishlistCount = count;
    });
  }

  @HostListener("window:scroll", [])
  public onWindowScroll(): void {
    this.isScrolled = window.scrollY > 50;
  }

  public toggleNavbar(): void {
    this.navbarOpen = !this.navbarOpen;
  }

  public closeModal(): void {
    this.showModal = false;
  }

  public closeProfileModal(): void {
    this.showProfileModal = false;
  }

  public onBackdropClick(event: MouseEvent): void {
    const modalContent = document.querySelector('.modal-content');
    if (modalContent && !modalContent.contains(event.target as Node)) {
      this.closeModal();
      this.closeProfileModal();
    }
  }

  public stopPropagation(event: MouseEvent): void {
    event.stopPropagation();
  }



  public loadCategories(): void {
    this.categoryService.getCategoryList().subscribe((allCategories: CategoryModel[]) => {
      const buildCategoryTree = (parentId: number): CategoryModel[] => {
        return allCategories
          .filter(category => category.parentId === parentId)
          .map(category => ({
            ...category,
            expanded: false,
            subCategories: buildCategoryTree(category.id)
          }));
      };
      this.categoryList = buildCategoryTree(0);
    });
  }
  

  public navigateToProduct(categoryId: number): void {
    this.router.navigate(['/product'], { queryParams: { categoryId } });
    // Close all open dropdowns
    const dropdowns = document.querySelectorAll('.dropdown-menu.show, .dropdown.show');
    dropdowns.forEach(dropdown => {
      dropdown.classList.remove('show');
    });
  }

  public toggleCategory(category: CategoryModel, event: Event): void {
    event.preventDefault();
    this.selectedCategoryId = this.selectedCategoryId === category.id ? null : category.id;
    this.categoryList.forEach(cat => {
      cat.expanded = cat.id === this.selectedCategoryId;
    });
  }
}
