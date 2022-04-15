from textwrap import fill
from turtle import pos
from django.http.response import Http404
from django.shortcuts import get_object_or_404, redirect, render
from .models import *
from .forms import Comment_Form, Post_Form, Tag_Form, UserUpdateForm
from django.views.generic import View, ListView
from django.contrib.auth.models import User
from django.contrib.auth.decorators import login_required, permission_required
from django.contrib.auth.mixins import LoginRequiredMixin, PermissionRequiredMixin
from django.db.models import Q
from django.core.paginator import Paginator

def index(request):
    search_request = request.GET.get('search', '')
    if search_request:
        post_list = Post.objects.filter(Q(header__icontains=search_request) | Q(body__icontains=search_request))
    else:
        post_list = Post.objects.all()

    paginator = Paginator(post_list, 5)

    page_number = request.GET.get('page', 1)
    page = paginator.get_page(page_number)

    is_paginated = page.has_other_pages()

    if page.has_previous():
        prev_url = '?page={}'.format(page.previous_page_number())
    else:
        prev_url = ''

    if page.has_next():
        next_url = '?page={}'.format(page.next_page_number())
    else:
        next_url = ''

    context_obj = {'posts_var':page, 
                'is_paginated': is_paginated,
                'prev_url': prev_url,
                'next_url': next_url}
    return render(request, 'news/index.html', context=context_obj)

def post_detail(request, post_id):
    try:
        post = Post.objects.get(id = post_id)
        # context = {'post':post, 'form':Comment_Form}
        comment_list = Comment.objects.filter(fk_post = post_id)
    except Post.DoesNotExist:
        return Http404('Статья не найдена')

    paginator = Paginator(comment_list, 2)

    page_number = request.GET.get('page', 1)
    page = paginator.get_page(page_number)

    is_paginated = page.has_other_pages()

    if page.has_previous():
        prev_url = '?page={}'.format(page.previous_page_number())
    else:
        prev_url = ''

    if page.has_next():
        next_url = '?page={}'.format(page.next_page_number())
    else:
        next_url = ''

    context_obj = {'posts_var':page, 
                'is_paginated': is_paginated,
                'prev_url': prev_url,
                'next_url': next_url,
                'post':post,
                'form':Comment_Form}
    return render(request, 'news/post_detail.html', context=context_obj)

@permission_required('comment.cant_add_comment')
@login_required
def add_comment(request, post_id):
    form = Comment_Form(request.POST)
    post = get_object_or_404(Post, id=post_id)

    if form.is_valid():
        comment = Comment()
        comment.comment = form.cleaned_data['comment']
        comment.author = request.user.username
        comment.fk_post = post
        comment.save()
    return redirect(post.get_absolute_url())

class PostCreateview(PermissionRequiredMixin, LoginRequiredMixin, View):
    permission_required = ('post.can_add_post', )
    raise_exception = True

    def get(self, request, *args, **kwargs):
        form = Post_Form()
        return render(request, 'news/post_create_form.html', {'form':form})

    def post(self, request, *args, **kwargs):
        filled_form = Post_Form(request.POST)

        if filled_form.is_valid():
            new_post = filled_form.save(commit=False)
            # post_m2m = filled_form.save_m2m()
            new_post.author = request.user.username
            # new_post.courses = post_m2m
            new_post.save()
            filled_form.save_m2m()
            return redirect(new_post)
        return render(request, 'news/post_create_form.html', {'form':filled_form})

class PostUpdate(LoginRequiredMixin, View):
    def get(self, request, id_post):
        post = Post.objects.get(pk=id_post)
        form = Post_Form(instance=post)
        return render(request, 'news/post_update_form.html', context={'form': form, 'obj': post})

    def post(self, request, id_post):
        post = Post.objects.get(pk=id_post)
        form = Post_Form(request.POST, instance=post)

        if form.is_valid():
            new_obj = form.save()
            return redirect(new_obj)
        return render(request, 'news/post_update_form.html', context={'form': form, 'obj': post})

class PostDelete(LoginRequiredMixin, View):
    def get(self, request, id_post):
        post = Post.objects.get(pk=id_post)
        return render(request, 'news/post_delete_form.html', context={'obj': post})

    def post(self, request, id_post):
        post = Post.objects.get(pk=id_post)
        post.delete()
        return redirect(reverse('index_url'))

class UserUpdateView(LoginRequiredMixin, View):
    def get(self, request):
        data_obj = User.objects.get(username=request.user.username)
        bound_form = UserUpdateForm(instance=data_obj)
        return render(request, 'news/user_account.html', context={'form': bound_form, 'obj': data_obj})
    
    def post(self, request):
        data_obj = User.objects.get(username=request.user.username)
        bound_form = UserUpdateForm(request.POST, instance=data_obj)

        if bound_form.is_valid():
            bound_form.save()
            return redirect('profile_detail_url')
        return render(request, 'news/user_account.html', context={'form': bound_form, 'obj': data_obj})

class UserPostListView(LoginRequiredMixin, ListView):
    model = Post
    template_name ='news/posts_list.html'

    def get_queryset(self):
        return Post.objects.filter(author = self.request.user).order_by('time_post')