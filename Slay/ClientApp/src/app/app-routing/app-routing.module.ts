import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileComponent }      from '../components/profile/profile.component';
import { ProfilePostsComponent } from '../components/profile/profile-posts/profile-posts.component';
import { ScrapbookComponent } from '../components/profile/scrapbook/scrapbook.component';

const rootRoutes: Routes = [
  { path: 'profile', component: ProfileComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' }
];

const profileRoutes: Routes = [
  { 
    path: 'profile',
    component: ProfileComponent,
    children: [
      { path: 'posts', component: ProfilePostsComponent, outlet: 'profile' },
      { path: 'scraps', component: ScrapbookComponent, outlet: 'profile' },
      { path: '', pathMatch: 'full', redirectTo: 'posts' }
    ]
   }
];

@NgModule({
  imports: [ RouterModule.forRoot(rootRoutes), RouterModule.forChild(profileRoutes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}