using System;
using System.Collections.Generic;

namespace examSys
{
    class ans
    {
        public int id;
        public string text;
        public ans(int i, string t)
        {
            id = i;
            text = t;
        }
        public override string ToString()
        {
            return id + "-" + text;
        }
    }

    class ques : ICloneable, IComparable<ques>
    {
        public string head;
        public string body;
        public int mark;
        public ans[] anslist;
        public ans rightAns;

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public int CompareTo(ques other)
        {
            return mark.CompareTo(other.mark);
        }

        public override string ToString()
        {
            return head + "\n" + body;
        }
    }

    class TFQ : ques
    {
        public TFQ()
        {
            anslist = new ans[2];
            anslist[0] = new ans(1, "True");
            anslist[1] = new ans(2, "False");
        }
    }

    class MCQQ : ques
    {
        public MCQQ(int count)
        {
            anslist = new ans[count];
        }
    }

    class exam
    {
        public int time;
        public int qnum;
        public List<ques> qs = new List<ques>();

        public virtual void show()
        {
        }
    }

    class pracExam : exam
    {
        public override void show()
        {
            int i = 1;
            foreach (var q in qs)
            {
                Console.WriteLine("Q" + i + ": " + q);
                foreach (var a in q.anslist)
                {
                    Console.WriteLine(a);
                }
                Console.Write("Your answer: ");
                Console.ReadLine();
                Console.WriteLine("Correct: " + q.rightAns);
                Console.WriteLine();
                i++;
            }
        }
    }

    class finalExam : exam
    {
        public int grade = 0;
        public override void show()
        {
            DateTime start = DateTime.Now;
            int i = 1;
            int totalMarks = 0;

            foreach (var q in qs)
            {
                totalMarks += q.mark;
                Console.WriteLine("Question " + i + " : " + q.body);
                foreach (var a in q.anslist)
                {
                    Console.WriteLine(a);
                }
                Console.Write("Your answer: ");
                int userAnsId = int.Parse(Console.ReadLine());
                string userAnsText = q.anslist[userAnsId - 1].text;
                string correctAnsText = q.rightAns.text;

                Console.WriteLine("Your Answer => " + userAnsText);
                Console.WriteLine("Your Answer => " + correctAnsText);

                if (q.rightAns.id == userAnsId)
                {
                    grade += q.mark;
                }
                i++;
            }

            DateTime end = DateTime.Now; 
            TimeSpan duration = end - start;

            Console.WriteLine("Your Grade is " + grade + " from " + totalMarks);
            Console.WriteLine("Time = " + duration);
            Console.WriteLine("Thank you");
        }
    }

    class subj
    {
        public int id;
        public string name;
        public exam ex;
        public void create(exam e)
        {
            ex = e;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            subj s = new subj();
            s.id = 1;
            s.name = "OOP";

            Console.WriteLine("Please Enter the type of Exam (1 for Practical | 2 for Final)");
            int t = int.Parse(Console.ReadLine());

            Console.WriteLine("Please Enter the Time For Exam from (30 min to 180 min)");
            int time = int.Parse(Console.ReadLine());

            Console.WriteLine("Please Enter the Number of questions");
            int numQ = int.Parse(Console.ReadLine());

            exam e;
            if (t == 1)
                e = new pracExam();
            else
                e = new finalExam();

            e.time = time;
            e.qnum = numQ;

            for (int i = 0; i < numQ; i++)
            {
                Console.WriteLine("Please Enter the Type of Question (1 for MCQ | 2 For True | False)");
                int qtype = int.Parse(Console.ReadLine());

                Console.WriteLine("Please Enter Question Body");
                string body = Console.ReadLine();

                Console.WriteLine("Please Enter Question Mark");
                int mark = int.Parse(Console.ReadLine());

                ques q;
                if (qtype == 1)
                {
                    MCQQ mq = new MCQQ(3);
                    mq.head = "MCQ Question";
                    mq.body = body;
                    mq.mark = mark;

                    for (int c = 0; c < 3; c++)
                    {
                        Console.WriteLine("Please Enter Choice number " + (c + 1));
                        string choice = Console.ReadLine();
                        mq.anslist[c] = new ans(c + 1, choice);
                    }

                    Console.WriteLine("Please Enter the right answer id");
                    int right = int.Parse(Console.ReadLine());
                    mq.rightAns = mq.anslist[right - 1];
                    q = mq;
                }
                else
                {
                    TFQ tf = new TFQ();
                    tf.head = "True/False Question";
                    tf.body = body;
                    tf.mark = mark;

                    Console.WriteLine("Please Enter the right answer id");
                    int right = int.Parse(Console.ReadLine());
                    tf.rightAns = tf.anslist[right - 1];
                    q = tf;
                }

                e.qs.Add(q);
            }

            s.create(e);
            s.ex.show();
        }
    }
}
