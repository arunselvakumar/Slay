import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

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
    RightBannerAdunitComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
