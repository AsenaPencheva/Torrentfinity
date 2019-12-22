import { NgModule } from "@angular/core";
import { ImageComponent } from "./image.component";
import { COLUMNS_PROVIDER } from "./columns-provider";
import { BrowserModule }    from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
    imports: [
        BrowserModule,
        HttpClientModule,
      ],
    declarations: [
        ImageComponent
    ],
    entryComponents: [
        // The component needs to be registered here as it is instantiated dynamically.
        ImageComponent
    ],
    providers: [
        COLUMNS_PROVIDER
    ]
})
export class GridExtenderModule { /* empty */ }
