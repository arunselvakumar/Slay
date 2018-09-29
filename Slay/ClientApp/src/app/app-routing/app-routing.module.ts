import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ProfileComponent } from "../components/profile/profile.component";
import { ProfilePostsComponent } from "../components/profile/profile-posts/profile-posts.component";
import { ScrapbookComponent } from "../components/profile/scrapbook/scrapbook.component";
import { FollowingListComponent } from "../components/profile/following-list/following-list.component";
import { FollowersListComponent } from "../components/profile/followers-list/followers-list.component";
import { HomeComponent } from '../components/dashboard/home/home.component';

const rootRoutes: Routes = [
  {
    path: ":userId",
    component: ProfileComponent,
    children: [
      { path: "posts", component: ProfilePostsComponent },
      { path: "scraps", component: ScrapbookComponent },
      { path: "following", component: FollowingListComponent },
      { path: "followers", component: FollowersListComponent },
      { path: "", component: ScrapbookComponent, pathMatch: "full" },
      { path: "**", redirectTo: "" }
    ]
  },
  { path: "", component: HomeComponent },
  { path: "**", redirectTo: "" }
];

@NgModule({
  imports: [RouterModule.forRoot(rootRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
