import { CommonModule } from "@angular/common";
import { Component, inject, TemplateRef } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { RouterModule, RouterOutlet } from "@angular/router";
import { FooterComponent } from "./components/footer/footer.component";
import { HeaderComponent } from "./components/header/header.component";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { ToastService } from "./services/toast.service";


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterModule , FormsModule,CommonModule,HeaderComponent, FooterComponent,NgbModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'ECommerce.Web';
  toastService = inject(ToastService);

 isTemplate(toast: any) {
  return toast.textOrTpl instanceof TemplateRef;
}
}
