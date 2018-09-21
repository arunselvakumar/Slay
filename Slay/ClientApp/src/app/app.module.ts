import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AdsenseModule } from 'ng2-adsense';

import { AppComponent } from './app.component';
import { NavigationHeaderComponent } from './navigation-header/navigation-header.component';
import { PostListComponent } from './post-list/post-list.component';
import { PostComponent } from './post/post.component';
import { PostDetailsComponent } from './post-details/post-details.component';
import { HeaderComponent } from './header/header.component';
import { TrendingComponent } from './trending/trending.component';
import { FeedSuggestionsComponent } from './feed-suggestions/feed-suggestions.component';
import { MemePostComponentComponent } from './meme-post-component/meme-post-component.component';
import { RightBannerAdunitComponent } from './right-banner-adunit/right-banner-adunit.component';
import { ProfileComponent } from './components/profile/profile.component';
import { AppRoutingModule } from './app-routing/app-routing.module';
import { ProfileCoverComponent } from './components/profile/profile-cover/profile-cover.component';
import { ProfileNavComponent } from './components/profile/profile-nav/profile-nav.component';
import { ProfilePostsComponent } from './components/profile/profile-posts/profile-posts.component';
import { ProfilePostComponent } from './components/profile/profile-post/profile-post.component';
import { ScrapbookComponent } from './components/profile/scrapbook/scrapbook.component';
import { ScrapComponent } from './components/profile/scrap/scrap.component';

@NgModule({
  declarations: [
    AppComponent,
    NavigationHeaderComponent,
    PostListComponent,
    PostComponent,
    PostDetailsComponent,
    HeaderComponent,
    TrendingComponent,
    FeedSuggestionsComponent,
    MemePostComponentComponent,
    RightBannerAdunitComponent,
    ProfileComponent,
    ProfileCoverComponent,
    ProfileNavComponent,
    ProfilePostsComponent,
    ProfilePostComponent,
    ScrapbookComponent,
    ScrapComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AdsenseModule.forRoot({
      adClient: 'ca-pub-6185517953080782',
      adSlot: 7259870550,
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
