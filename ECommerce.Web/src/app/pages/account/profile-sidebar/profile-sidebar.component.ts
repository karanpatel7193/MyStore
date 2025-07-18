import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SessionService } from '../../../services/session.service';

@Component({
  selector: 'app-profile-sidebar',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './profile-sidebar.component.html',
  styleUrl: './profile-sidebar.component.scss'
})
export class ProfileSidebarComponent {

  sessionService = inject(SessionService);
  logout() {
    this.sessionService.logout();
  }

  
}
