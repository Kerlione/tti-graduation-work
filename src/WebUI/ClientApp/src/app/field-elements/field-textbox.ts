import { QuestionBase } from '../models/question-base';

export class TextboxField extends QuestionBase<string> {
    controlType = 'textbox';
    type: string;

    constructor(options: {} = {}) {
        super(options);
        this.type = options['type'] || '';
    }
}
