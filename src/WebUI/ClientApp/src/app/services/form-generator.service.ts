import { Injectable } from '@angular/core';
import { QuestionBase } from '../models/question-base';
import { TextboxField } from '../field-elements/field-textbox';
import { DropdownField } from '../field-elements/field-dropdown';

@Injectable({
  providedIn: 'root'
})
export class FormGeneratorService {

  constructor() { }

  public generateForm(stepType: number, selectData?: any[]): QuestionBase<any>[] {
    let form: QuestionBase<any>[] = [];
    switch (stepType) {
      // Topic Approval
      case 1: {
        form.push(
          new TextboxField({
            key: 'title_EN',
            label: 'Topic EN',
            value: 'Topic name in English',
            required: true,
            order: 2
          }),
          new TextboxField({
            key: 'title_RU',
            label: 'Topic RU',
            value: 'Topic name in Russian',
            required: true,
            order: 3
          }),
          new TextboxField({
            key: 'title_LV',
            label: 'Topic LV',
            value: 'Topic name in Latvian',
            required: true,
            order: 1
          }),
          new DropdownField({
            key: 'supervisorId',
            label: 'Supervisor',
            options: selectData,
            required: true,
            order: 4
          })
        );
      }
    }
    return form.sort((a, b) => a.order - b.order);
  }
}
