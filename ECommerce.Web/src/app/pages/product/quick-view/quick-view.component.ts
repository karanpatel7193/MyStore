import { Component, Input, AfterViewInit, ViewChild, ElementRef, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ImageUrlPipe } from "../../../globalization/pipe/image-url-pipe";

@Component({
  selector: 'app-quick-view',
  standalone: true,
  imports: [CommonModule, FormsModule, ImageUrlPipe],
  templateUrl: './quick-view.component.html',
  styleUrls: ['./quick-view.component.scss']
})
export class QuickViewComponent implements AfterViewInit {
  @Input() product: any;
  @ViewChild('quickViewModal', { static: false }) quickViewModal!: ElementRef;
  private modalInstance: any = null;
  private isBrowser: boolean;

  constructor(@Inject(PLATFORM_ID) private platformId: any) {
    this.isBrowser = isPlatformBrowser(this.platformId);
  }

  async ngAfterViewInit(): Promise<void> {
    if (this.isBrowser) {
      const bootstrap = await import('bootstrap'); // Dynamically import Bootstrap only in the browser
      if (this.quickViewModal?.nativeElement) {
        this.modalInstance = new bootstrap.Modal(this.quickViewModal.nativeElement);
      }
    }
  }

  openModal(): void {
    if (this.isBrowser && this.modalInstance) {
      this.modalInstance.show();
    }
  }

  closeModal(): void {
    if (this.isBrowser && this.modalInstance) {
      this.modalInstance.hide();
    }
  }

}
