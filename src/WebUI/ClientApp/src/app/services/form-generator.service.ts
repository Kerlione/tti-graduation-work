import { Injectable } from '@angular/core';
import { QuestionBase } from '../models/question-base';
import { TextboxField } from '../field-elements/field-textbox';
import { DropdownField } from '../field-elements/field-dropdown';
import { StepData } from '../models/step-data';
import { StepCardData } from '../models/step-card-data';
import { RadioField } from '../field-elements/field-radio';

@Injectable({
  providedIn: 'root'
})
export class FormGeneratorService {

  constructor() { }

  public generateForm(stepType: number, selectData?: any[], readonly?: boolean): StepData {
    let data = new StepData();
    let form: QuestionBase<any>[] = [];
    switch (stepType) {
      case 0: {
        data.isForm = false;
        data.cardData = new StepCardData();
        data.cardData.title = 'Welcome to Graduation Paper process';
        data.cardData.content = `<p><b>This is the welcome screen of the step-based graduation paper preparation.</b></p>
        <p>This step provides basic information about the procedure of step following.</p>
        <p>This guide will help you to successfully finish the graduation, 
        if you follow the steps and the supervisor helps you and validates your work.</p>
        <p>Example steps are:</p>
        <ul>
          <li>Topic selection for graduation paper</li>
          <li>Thesis topic defence</li>
          <li>Thesis development</li>
          <li>Thesis pre-defence</li>
          <li>Thesis upload</li>
          <li>Plagiarism check</li>
          <li>Thesis delivery</li>
          <li>Thesis defence</li>
        </ul>`;
        break;
      }
      // Topic Approval
      case 1: {
        form.push(
          new TextboxField({
            key: 'title_EN',
            label: 'Topic EN',
            value: 'Topic name in English',
            required: true,
            order: 2,
            readonly: readonly
          }),
          new TextboxField({
            key: 'title_RU',
            label: 'Topic RU',
            value: 'Topic name in Russian',
            required: true,
            order: 3,
            readonly: readonly
          }),
          new TextboxField({
            key: 'title_LV',
            label: 'Topic LV',
            value: 'Topic name in Latvian',
            required: true,
            order: 1,
            readonly: readonly
          }),
          new DropdownField({
            key: 'supervisorId',
            label: 'Supervisor',
            options: selectData,
            required: true,
            order: 4,
            readonly: readonly
          }),
          new RadioField({
            key: 'paperType',
            label: 'Paper type',
            options: [
              { key: 0, value: 'Bachelor' },
              { key: 1, value: 'Master' }
            ],
            required: true,
            order: 5,
            readonly: readonly
          })
        );
        data.formData = form.sort((a, b) => a.order - b.order);
        data.isForm = true;
        break;
      }
      case 2: {
        data.isForm = false;
        data.cardData = new StepCardData();
        data.cardData.title = 'Topic approval step';
        data.cardData.content = `<p><b>Main prerequisite for this step is the completed, approved by Supervisor and Dean of faculty, and delivered Thesis Topic selection form.</b></p>
        <p>This step requires student to prepare the presentation of his/her selected and approved topic for the thesis of respective Degree.
        The presentation time limit is <b>3-5 minutes</b>.
        The supervisor comitee is going to listen to the topic presentation.</p>
        <p>Presentation must include:</p>
        <ul>
          <li>Topic name, student information (Name, Surname, Group, Supervisor)</li>
          <li>Revelance of topic area</li>
          <li>Achievable goals</li>
          <li>Tasks for the thesis work</li>
          <li>Implementation tools and techniques</li>
          <li>Expected outcomes</li>
        </ul>
        All the attending students will be splited into groups and distributed between the appointed lecture halls.
        Approximate presenatation day is <b>December 14, 2020, starting at 16:30.</b>`;
        break;
      }
      case 3: {
        data.isForm = false;
        data.cardData = new StepCardData();
        data.cardData.title = 'Thesis preparation step';
        data.cardData.content = `This step replicates the process of iterative reviews of work done on thesis between supervisor and student.
        Progress tracking is determined by accomplishing the milestones which were set as goals for the graduation paper.
        The template for the final document with described steps of graduation paper can be downloaded 
        <a href="https://www.google.com">here</a>`;
        break;
      }
      case 4: {
        data.isForm = false;
        data.cardData = new StepCardData();
        data.cardData.title = 'RatSif conference';
        data.cardData.content = `<p>This step requires student of Master degree to participate and present his/her research
        at the RatSif conference, hosted in the TTI.</p>
        Detailed requirements for the presentation for conference, registration procedure and etc. 
        can be viewed on the RatSif conference homepage: <a href="http://ratsif2020s.tsi.lv/"> RatSif </a>`;
        break;
      }
      case 5: {
        data.isForm = false;
        data.cardData = new StepCardData();
        data.cardData.title = "Rector's Order";
        data.cardData.content = `<p>This step is achieved and can be completed by student who has <b>no academical or financial depts</b>.</p>
        <p>The review is required for the supervisor to confirm, that student is allowed to participate in next steps.</p>`;
        break;
      }
      case 6: {
        data.isForm = false;
        data.cardData = new StepCardData();
        data.cardData.title = `Thesis Pre-defence`;
        data.cardData.content = `<p>The step represents the check of performed work under the topic of thesis.</p>
        <p>The students are allowed to attend the pre-defence event only by providing the work paper, being nearly finished, except the translation (if necessary).</p>
        <p>Pre-defence event requires work presentation with following requirements:<p>
        <ul>
        <li>Covering slide with topic, student and supervisor information</li>
        <li>Revelance of topic</li>
        <li>Goals and tasks for thesis</li>
        <li>Gathered functional and non-functional requirements</li>
        <li>Technology stack</li>
        <li>Architecture (2-3 slides)</li>
        <li>System workflow demonstration</li>
        <li>Testing</li>
        <li>Conclusion and summary</li>
        </ul>
        <p>The preseting time for student must not exceed <b>5-7 minutes</b>. Also, the comitee will ask questions regarding the paper.
        The student must be prepared for the questions.</p>
        <p>Supervisor validates the presentation of student and provides feedback regarding overall readiness</p>
        <p>For the year 2020, the pre-defence days are <b>27.05.2020-29.05.2020</b>`;
        break;
      }
      case 7: {
        data.isForm = false;
        data.cardData = new StepCardData();
        data.cardData.title = `Thesis upload`;
        data.cardData.content = `<p>
        As the step result, the student must have the graduation paper with annotations uploaded.
    </p>
    <p>
        The following items are required:
    </p>
    <ul>
        <li>
            Annotation - in three languages - English, Latvian and Russian
        </li>
        <li>
            Graduation paper main content
        </li>
        <li>
            Graduation paper general description (mainly numbers): document parameters - pages, tables, pictures, literature sources, attachments
        </li>
        <li>
            Graduation paper tags - on all three languages
        </li>
    </ul>`;
        break;
      }
      case 8: {
        data.isForm = false;
        data.cardData = new StepCardData();
        data.cardData.title = `Plagiarism check`;
        data.cardData.content = `<p>
        As the step result, the student's paper is verified against plagiarism using internal services.
    </p>
    <p>
        The supervisor is responsible for notifying student about results of check.
    </p>
    `;
        break;
      }
      case 9: {
        data.isForm = false;
        data.cardData = new StepCardData();
        data.cardData.title = `Thesis review`;
        data.cardData.content = `<p>
        For Master's degree students, the graduation paper review is required.
    </p>
    <p>
        The student selects the person to review the developed thesis.
        After student selects the reviewer, the step is considered as In progress.
    </p>
    <p>
        When the review is received from selected person, the step can be assigned for review of supervisor.
    </p>
    <p>
        The review gets attached to the final work. Supervisor confirms the validity of the review.
    </p>
    `;
        break;
      }
      case 10: {
        data.isForm = false;
        data.cardData = new StepCardData();
        data.cardData.title = `Thesis delivery`;
        data.cardData.content = `<p>
        The step is devoted to the preparation of final work in-paper and its delivery to the TTI.
    </p>
    <p>
        The student has to print and tie the graduation paper.
    </p>
    <p>
        The final structure of paper is:
    </p>
    <ul>
        <li>Covering page - taken from personal card of student <a href="https://intra.tsi.lv/">here</a></li>
        <li>Uploaded graduation paper into system <a href="http://services.tsi.lv/gradeworks">here</a></li>
    </ul>
    <p>
        The physical storage device with developed application also must be provided.
    </p>
    <p>
        Once the delivery is done, the student can send the step to review.
    </p>
    <p>
        Supervisor verifies that the delivery was performed and the work is <b>accepted</b>.
    </p>
    `;
        break;
      }
      case 11: {
        data.isForm = false;
        data.cardData = new StepCardData();
        data.cardData.title = `Thesis Defence`;
        data.cardData.content = `<p>The step represents the final and main check of performed work under the topic of thesis.</p>
        <p>The students are allowed to attend the defence event only after the work got delivered to the TTI.</p>
        <p>Defence event requires work presentation with following requirements:<p>
        <ul>
        <li>Covering slide with topic, student and supervisor information</li>
        <li>Revelance of topic</li>
        <li>Goals and tasks for thesis</li>
        <li>Gathered functional and non-functional requirements</li>
        <li>Technology stack</li>
        <li>Architecture (2-3 slides)</li>
        <li>System workflow demonstration</li>
        <li>Testing</li>
        <li>Conclusion and summary</li>
        </ul>
        <p>The preseting time for student must not exceed <b>5-7 minutes</b>. Also, the comitee will ask questions regarding the paper.
        The student must be prepared for the questions.</p>
        <p>Supervisor validates the presentation of student and provides feedback regarding overall readiness</p>
        <p>The participation in the event is <b>mandatory</b>. As the student count is high, the event is split into two days.
        The student has to apply for one of the available dates</p>
        <p>For the year 2020, the defence days are <b>16.06.2020-19.06.2020</b></p>`;
        break;
      }
    }
    return data;
  }
}
