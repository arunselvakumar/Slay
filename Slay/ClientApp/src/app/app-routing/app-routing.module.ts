import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ProfileComponent } from "../components/profile/profile.component";
import { ProfilePostsComponent } from "../components/profile/profile-posts/profile-posts.component";
import { ScrapbookComponent } from "../components/profile/scrapbook/scrapbook.component";

const rootRoutes: Routes = [
  {
    path: ":userId",
    component: ProfileComponent,
    children: [
      { path: "posts", component: ProfilePostsComponent },
      { path: "scraps", component: ScrapbookComponent },
      { path: "", component: ScrapbookComponent, pathMatch: "full" },
      { path: "**", redirectTo: "" }
    ]
  },
  { path: "", redirectTo: "home", pathMatch: "full" }
];

@NgModule({
  imports: [RouterModule.forRoot(rootRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
