import { Component, OnInit } from "@angular/core";
import { DataContextComponent, DataContext } from "progress-sitefinity-adminapp-sdk/app/api/v1";
import { HttpClient } from '@angular/common/http';

// Get a collection with images in base64 format from image-data.json file

// const imageData = require("./image-data.json");

@Component({
    template: require("./image.component.html")
})
export class ImageComponent implements OnInit, DataContextComponent {
    constructor(private httpClient: HttpClient) { }
    context: DataContext;
    protected imageHeight = 50;
    protected imageSource;

    ngOnInit() {
        const dataItem = this.context.dataItem;
        const imageIdentifier = this.getImageIdentifier(dataItem);
        // Get this image's source for rendering
        if (imageIdentifier) {
            let set = dataItem.metadata.setName;
            let id = dataItem.data.Id;
            const oDataEndpoint = `https://localhost:44308/api/default/${set}(${id})?$select=*&$expand=Image&sf_provider=OpenAccessProvider&sf_culture=en&sf_fallback_prop_names=Image.Url`;

            this.httpClient.get(oDataEndpoint).subscribe((data: any) => {
                this.imageSource = data.Image[0].Url;
            }, error => {
                this.imageSource = 'Draft';
            });
        }
    }

    // Returns a random number between min (inclusive) and max (exclusive)
    getImageIdentifier(dataItem) {
        if (dataItem.data.hasOwnProperty('Image')) {
            //  return dataItem.title
            return 1;
        }

        return null;
    }
}
